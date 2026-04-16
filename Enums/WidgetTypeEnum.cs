namespace WindowsFormsApp1.Enums
{
    public enum WidgetTypeEnum
    {
        Animation = 1,
        Caption = 2,
        Text= 3,
        Selection= 4
    }

    public static class WidgetTypeEnumExtension
    {
        public static bool IsWidget(this WidgetTypeEnum @enum)
        {
            if (@enum == WidgetTypeEnum.Animation ||
                @enum == WidgetTypeEnum.Caption ||
                @enum == WidgetTypeEnum.Text ||
                @enum == WidgetTypeEnum.Selection)
            {
                return true;
            }

            return false;
        }
    }
}
