using System;

namespace Tidalwav.Editor
{
    internal class Loop
    {
        private long _startSample;
        private long _endSample;

        public long StartSample
        {
            get => _startSample;
            set => _startSample = Math.Max(value, 0);
        }

        public long EndSample
        {
            get => _endSample;
            set => _endSample = Math.Max(value, _startSample);
        }
    }
}