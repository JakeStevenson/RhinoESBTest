using System;

namespace RhinoESBTest.Core.Messages
{
    public class VideoToEncode
    {
        public DateTime RequestDate { get; set; }
        public string ID { get; set; }
        public string FileName { get; set; }
    }
}