using System.Collections.Generic;

namespace WindowsFormsApp1.Models.Animation
{
    public class AnimationLineExtract
    {
        public List<string> Images { get; set; }
        public int ImageCount { get =>  Images.Count;  }
        public int TimeInterval { get; set; }
    }
}
