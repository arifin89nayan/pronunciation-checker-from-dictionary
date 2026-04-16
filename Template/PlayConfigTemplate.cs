namespace WindowsFormsApp1.Template
{
    public class PlayConfigTemplate
    {
        public static string BuildFromGenericTemplate(
          string animationAudio, string animation_Images, string seenAudio,
          string seenImages)
        {
            return PlayConfig
                  
                  .Replace("Animation_Images", animation_Images)
                  .Replace("Seen_Audio", seenAudio)
                  .Replace("Seen_Images", seenImages);
        }
         

        public const string PlayConfig = @"V:002
2
<1><1>
<1><1><1><1><1><0>
<1>
<1900>
<M><1><0000014BE.WMA>
<B><*0>
<A><0><2>
<W>
<P><Animation_Images><0><0><240><320><2><40><1>
<E>
<2><1>
<0><0><0><0><0><0>
<1>
<53000>
<M><1><Seen_Audio>
<B><*0>
<A><0><0>
<W>
<P><Seen_Images><0><296><240><23><3><80><1>
<W>
<C><8bdc><0><0><240><295><3><360><861><1000><4900><200><0><1>
<E>";

        public static string BuildFirstPartFromGenericTemplate(
  string animationAudio, string animation_Images)
        {
            return PlayConfigPart1
                  
                  .Replace("Animation_Images", animation_Images);
        }

        //        public const string PlayConfigPart1 = @"V:002
        //2
        //<1><1>
        //<1><1><1><1><1><0>
        //<1>
        //<1900>
        //<M><1><0000014BE.WMA>
        //<B><*0>
        //<A><0><2>
        //<W>
        //<P><Animation_Images><0><0><240><320><Animation_Image_Number><Animation_Time><1>
        //<E>";
        public const string PlayConfigPart1 = @""+"V:002\r\n" +
                                              "2\r\n" +
                                              "<1>" +
                                              "<1>\r\n" +
                                              "<1>" +
                                              "<1>" +
                                              "<1>" +
                                              "<1>" +
                                              "<1>" +
                                              "<0>\r\n" +
                                              "<1>\r\n" +
                                              "<1900>\r\n" +
                                              "<M>" +
                                              "<1>" +
                                              "<000001F14.WMA>\r\n" +
                                              "<B>" +
                                              "<*0>\r\n" +
                                              "<A>" +
                                              "<0>" +
                                              "<2>\r\n" +
                                              "<W>\r\n" +
                                              "<P>" +
                                              "<Animation_Images>" +
                                              "<0>" +
                                              "<0>" +
                                              "<240>" +
                                              "<320>" +
                                              "<Animation_Image_Number>" +
                                              "<Animation_Time>" +
                                              "<1>\r\n" +
                                              "<E>";



        //        public const string PlayConfigPart2 = @"<2><1>
        //<0><0><0><0><0><0>
        //<1>
        //<Audio_Duration>
        //<M><1><Seen_Audio>
        //<B><*0>
        //<A><0><0>
        //<W>
        //<P><Seen_Images><0><296><240><23><Seen_Image_Count><80><1>
        //<W>
        //<C><Base_Image><0><0><240><295><Image_Block_Number><360><Final_Height><T1><T2><T3><0><1>
        //<E>";
        public const string PlayConfigPart2 ="<2><1>\r\n" +
                                              "<0><0><0><0><0><0>\r\n" +
                                              "<1>\r\n" +
                                              "<Audio_Duration>\r\n" +
                                              "<M>" +
                                              "<1>" +
                                              "<Seen_Audio>\r\n" +
                                              "<B>" +
                                              "<*0>\r\n" +
                                              "<A>" +
                                              "<0>" +
                                              "<0>\r\n" +
                                              "<W>\r\n" +
                                              "<P>" +
                                              "<Seen_Images>" +
                                              "<0>" +
                                              "<296>" +
                                              "<240>" +
                                              "<23>" +
                                              "<Seen_Image_Count>" +
                                              "<80>" +
                                              "<1>\r\n" +
                                              "<W>\r\n" +
                                              "<C>" +
                                              "<Base_Image>" +
                                              "<0>" +
                                              "<0>" +
                                              "<240>" +
                                              "<295>" +
                                              "<Image_Block_Number>" +
                                              "<360>" +
                                              "<Final_Height>" +
                                              "<T1>" +
                                              "<T2>" +
                                              "<T3>" +
                                              "<0>" +
                                              "<1>\r\n" +
                                              "<E>\r\n";


    }
}
