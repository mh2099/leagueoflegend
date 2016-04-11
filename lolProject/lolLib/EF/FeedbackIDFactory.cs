namespace lolLib.EF
{
    using System;

    public static class FeedbackIdFactory
    {
        private static UInt32 _lastId;
        public static UInt32 FeedbackId => CreateFeedbackId();

        private static UInt32 CreateFeedbackId()
        {
            if (_lastId == UInt32.MaxValue) _lastId = 0;
            _lastId++;
            return _lastId;
        }
    }
}