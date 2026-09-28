using System;
using System.Data.SQLite;
using System.IO;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public class GeneratedContentVersion
    {
        public long Id { get; set; }

        public long ContentItemId { get; set; }

        public int VersionNumber { get; set; }

        public string GeneratedText { get; set; }

        public string ModelName { get; set; }

        public string PromptVersion { get; set; }

        public bool IsEdited { get; set; }

        public bool IsApproved { get; set; }

        public string CreatedAt { get; set; }
    }


    public class ContentRepository
    {
        private readonly string _databasePath;
        private readonly string _connectionString;


        public ContentRepository(string databasePath)
        {
            _databasePath = databasePath;

            string folder =
                Path.GetDirectoryName(databasePath);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            _connectionString =
                $"Data Source={databasePath};Version=3;";
        }


        // ============================================================
        // INITIALIZE DATABASE
        // ============================================================

        public void Initialize()
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

                string contentTable = @"
CREATE TABLE IF NOT EXISTS ContentItems
(
    Id              INTEGER PRIMARY KEY AUTOINCREMENT,
    SourceId        TEXT NOT NULL,
    RowNumber       INTEGER NOT NULL,
    ContentType     TEXT NOT NULL,
    Topic           TEXT,
    LanguageName    TEXT,
    LanguageCode    TEXT,
    Voice           TEXT,
    TemplateName    TEXT,
    SourceText      TEXT,
    Status          TEXT,
    UpdatedAt       TEXT,

    UNIQUE(SourceId, ContentType)
);";


                string versionTable = @"
CREATE TABLE IF NOT EXISTS ContentVersions
(
    Id              INTEGER PRIMARY KEY AUTOINCREMENT,
    ContentItemId   INTEGER NOT NULL,
    VersionNumber   INTEGER NOT NULL,
    GeneratedText   TEXT NOT NULL,
    ModelName       TEXT,
    PromptVersion   TEXT,
    IsEdited        INTEGER DEFAULT 0,
    IsApproved      INTEGER DEFAULT 0,
    CreatedAt       TEXT NOT NULL,

    FOREIGN KEY(ContentItemId)
        REFERENCES ContentItems(Id)
);";


                using (var command =
                       new SQLiteCommand(
                           contentTable,
                           connection))
                {
                    command.ExecuteNonQuery();
                }

                using (var command =
                       new SQLiteCommand(
                           versionTable,
                           connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }


        // ============================================================
        // INSERT / UPDATE EXCEL SOURCE ITEM
        // ============================================================

        public long UpsertContentItem(
            ContentItem item)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

                long? existingId =
                    FindContentItemId(
                        connection,
                        item.SourceId,
                        item.ContentType);

                if (existingId.HasValue)
                {
                    string updateSql = @"
UPDATE ContentItems
SET
    RowNumber = @RowNumber,
    Topic = @Topic,
    LanguageName = @LanguageName,
    LanguageCode = @LanguageCode,
    Voice = @Voice,
    TemplateName = @TemplateName,
    SourceText = @SourceText,
    UpdatedAt = @UpdatedAt
WHERE Id = @Id;";

                    using (var command =
                           new SQLiteCommand(
                               updateSql,
                               connection))
                    {
                        AddSourceParameters(
                            command,
                            item);

                        command.Parameters.AddWithValue(
                            "@Id",
                            existingId.Value);

                        command.ExecuteNonQuery();
                    }

                    return existingId.Value;
                }


                string insertSql = @"
INSERT INTO ContentItems
(
    SourceId,
    RowNumber,
    ContentType,
    Topic,
    LanguageName,
    LanguageCode,
    Voice,
    TemplateName,
    SourceText,
    Status,
    UpdatedAt
)
VALUES
(
    @SourceId,
    @RowNumber,
    @ContentType,
    @Topic,
    @LanguageName,
    @LanguageCode,
    @Voice,
    @TemplateName,
    @SourceText,
    'Pending',
    @UpdatedAt
);";


                using (var command =
                       new SQLiteCommand(
                           insertSql,
                           connection))
                {
                    AddSourceParameters(
                        command,
                        item);

                    command.ExecuteNonQuery();
                }


                using (var command =
                       new SQLiteCommand(
                           "SELECT last_insert_rowid();",
                           connection))
                {
                    return Convert.ToInt64(
                        command.ExecuteScalar());
                }
            }
        }


        // ============================================================
        // CREATE GENERATED VERSION
        // ============================================================

        public long CreateVersion(
            long contentItemId,
            string generatedText,
            string modelName,
            string promptVersion,
            bool isEdited)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

                int nextVersion =
                    GetNextVersionNumber(
                        connection,
                        contentItemId);


                string sql = @"
INSERT INTO ContentVersions
(
    ContentItemId,
    VersionNumber,
    GeneratedText,
    ModelName,
    PromptVersion,
    IsEdited,
    IsApproved,
    CreatedAt
)
VALUES
(
    @ContentItemId,
    @VersionNumber,
    @GeneratedText,
    @ModelName,
    @PromptVersion,
    @IsEdited,
    0,
    @CreatedAt
);";


                using (var command =
                       new SQLiteCommand(
                           sql,
                           connection))
                {
                    command.Parameters.AddWithValue(
                        "@ContentItemId",
                        contentItemId);

                    command.Parameters.AddWithValue(
                        "@VersionNumber",
                        nextVersion);

                    command.Parameters.AddWithValue(
                        "@GeneratedText",
                        generatedText);

                    command.Parameters.AddWithValue(
                        "@ModelName",
                        modelName ?? "");

                    command.Parameters.AddWithValue(
                        "@PromptVersion",
                        promptVersion ?? "");

                    command.Parameters.AddWithValue(
                        "@IsEdited",
                        isEdited ? 1 : 0);

                    command.Parameters.AddWithValue(
                        "@CreatedAt",
                        DateTime.UtcNow.ToString("o"));

                    command.ExecuteNonQuery();
                }


                UpdateContentStatus(
                    connection,
                    contentItemId,
                    "Generated");


                using (var command =
                       new SQLiteCommand(
                           "SELECT last_insert_rowid();",
                           connection))
                {
                    return Convert.ToInt64(
                        command.ExecuteScalar());
                }
            }
        }


        // ============================================================
        // GET LATEST VERSION
        // ============================================================

        public GeneratedContentVersion GetLatestVersion(
            long contentItemId)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

                string sql = @"
SELECT
    Id,
    ContentItemId,
    VersionNumber,
    GeneratedText,
    ModelName,
    PromptVersion,
    IsEdited,
    IsApproved,
    CreatedAt
FROM ContentVersions
WHERE ContentItemId = @ContentItemId
ORDER BY VersionNumber DESC
LIMIT 1;";


                using (var command =
                       new SQLiteCommand(
                           sql,
                           connection))
                {
                    command.Parameters.AddWithValue(
                        "@ContentItemId",
                        contentItemId);

                    using (var reader =
                           command.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        return new GeneratedContentVersion
                        {
                            Id =
                                Convert.ToInt64(
                                    reader["Id"]),

                            ContentItemId =
                                Convert.ToInt64(
                                    reader["ContentItemId"]),

                            VersionNumber =
                                Convert.ToInt32(
                                    reader["VersionNumber"]),

                            GeneratedText =
                                reader["GeneratedText"]
                                    ?.ToString(),

                            ModelName =
                                reader["ModelName"]
                                    ?.ToString(),

                            PromptVersion =
                                reader["PromptVersion"]
                                    ?.ToString(),

                            IsEdited =
                                Convert.ToInt32(
                                    reader["IsEdited"]) == 1,

                            IsApproved =
                                Convert.ToInt32(
                                    reader["IsApproved"]) == 1,

                            CreatedAt =
                                reader["CreatedAt"]
                                    ?.ToString()
                        };
                    }
                }
            }
        }


        // ============================================================
        // APPROVE VERSION
        // ============================================================

        public void ApproveVersion(
            long contentItemId,
            long versionId)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

                using (var transaction =
                       connection.BeginTransaction())
                {
                    string clearSql = @"
UPDATE ContentVersions
SET IsApproved = 0
WHERE ContentItemId = @ContentItemId;";

                    using (var command =
                           new SQLiteCommand(
                               clearSql,
                               connection,
                               transaction))
                    {
                        command.Parameters.AddWithValue(
                            "@ContentItemId",
                            contentItemId);

                        command.ExecuteNonQuery();
                    }


                    string approveSql = @"
UPDATE ContentVersions
SET IsApproved = 1
WHERE
    Id = @VersionId
    AND ContentItemId = @ContentItemId;";

                    using (var command =
                           new SQLiteCommand(
                               approveSql,
                               connection,
                               transaction))
                    {
                        command.Parameters.AddWithValue(
                            "@VersionId",
                            versionId);

                        command.Parameters.AddWithValue(
                            "@ContentItemId",
                            contentItemId);

                        command.ExecuteNonQuery();
                    }


                    UpdateContentStatus(
                        connection,
                        contentItemId,
                        "Approved",
                        transaction);


                    transaction.Commit();
                }
            }
        }


        // ============================================================
        // HELPERS
        // ============================================================

        private SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection(
                _connectionString);
        }


        private long? FindContentItemId(
            SQLiteConnection connection,
            string sourceId,
            string contentType)
        {
            const string sql = @"
SELECT Id
FROM ContentItems
WHERE
    SourceId = @SourceId
    AND ContentType = @ContentType
LIMIT 1;";

            using (var command =
                   new SQLiteCommand(
                       sql,
                       connection))
            {
                command.Parameters.AddWithValue(
                    "@SourceId",
                    sourceId);

                command.Parameters.AddWithValue(
                    "@ContentType",
                    contentType);

                object value =
                    command.ExecuteScalar();

                if (value == null ||
                    value == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt64(value);
            }
        }


        private int GetNextVersionNumber(
            SQLiteConnection connection,
            long contentItemId)
        {
            const string sql = @"
SELECT COALESCE(MAX(VersionNumber), 0) + 1
FROM ContentVersions
WHERE ContentItemId = @ContentItemId;";

            using (var command =
                   new SQLiteCommand(
                       sql,
                       connection))
            {
                command.Parameters.AddWithValue(
                    "@ContentItemId",
                    contentItemId);

                return Convert.ToInt32(
                    command.ExecuteScalar());
            }
        }


        private void AddSourceParameters(
            SQLiteCommand command,
            ContentItem item)
        {
            command.Parameters.AddWithValue(
                "@SourceId",
                item.SourceId ?? "");

            command.Parameters.AddWithValue(
                "@RowNumber",
                item.RowNumber);

            command.Parameters.AddWithValue(
                "@ContentType",
                item.ContentType ?? "");

            command.Parameters.AddWithValue(
                "@Topic",
                item.Topic ?? "");

            command.Parameters.AddWithValue(
                "@LanguageName",
                item.Language ?? "");

            command.Parameters.AddWithValue(
                "@LanguageCode",
                item.LanguageCode ?? "");

            command.Parameters.AddWithValue(
                "@Voice",
                item.Voice ?? "");

            command.Parameters.AddWithValue(
                "@TemplateName",
                item.TemplateName ?? "");

            command.Parameters.AddWithValue(
                "@SourceText",
                item.SourceText ?? "");

            command.Parameters.AddWithValue(
                "@UpdatedAt",
                DateTime.UtcNow.ToString("o"));
        }


        private void UpdateContentStatus(
            SQLiteConnection connection,
            long contentItemId,
            string status,
            SQLiteTransaction transaction = null)
        {
            string sql = @"
UPDATE ContentItems
SET
    Status = @Status,
    UpdatedAt = @UpdatedAt
WHERE Id = @Id;";


            using (var command =
                   new SQLiteCommand(
                       sql,
                       connection,
                       transaction))
            {
                command.Parameters.AddWithValue(
                    "@Status",
                    status);

                command.Parameters.AddWithValue(
                    "@UpdatedAt",
                    DateTime.UtcNow.ToString("o"));

                command.Parameters.AddWithValue(
                    "@Id",
                    contentItemId);

                command.ExecuteNonQuery();
            }
        }
    }
}