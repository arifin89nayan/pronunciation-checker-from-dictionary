using System.IO;
using WindowsFormsApp1.Enums;
using WindowsFormsApp1.Global;
using WindowsFormsApp1.Models.Widgets;

namespace WindowsFormsApp1.Services
{
    public class TextFormService
    {
        public void GenerateMediaLines(Line media, string audioName)
        { 
            if (media.IsSkippedLine && media.Type == LineTypeEnum.Media)
            {
                GlobalConfigPropreties.Instance.ReplaceLineText(media, $"<M><1><{audioName}>");
            }
        }
        public void GenerateMediaLines(Line mediaDuration, Line media, string audioName)
        {
            if (mediaDuration.IsSkippedLine && mediaDuration.Type == LineTypeEnum.MediaDuration)
            {
                var generatedAudioDuration = AudioInfo.GetAudioDurationInTimeSpan(Path.Combine(GlobalProperties.OutputPath, audioName));
                var duration = generatedAudioDuration.TotalMilliseconds;

                GlobalConfigPropreties.Instance.ReplaceLineText(mediaDuration, $"<{duration}>");

            }

            if (media.IsSkippedLine && media.Type == LineTypeEnum.Media)
            {
                GlobalConfigPropreties.Instance.ReplaceLineText(media, $"<M><1><{audioName}>");
            }
        }
    }
}
