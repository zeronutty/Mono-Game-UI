using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTesting.Source
{
    public class FPSCounter
    {
        public FPSCounter()
        {

        }

        public long TotalFrames { get; private set; }
        public float TotalSeconds { get; private set; }
        public float AverageFramesPreSecond { get; private set; }
        public float CurrentFramesPreSecond { get; private set; }

        public const int MAXINUM_SAMPLES = 100;

        private Queue<float> _sampleBuffer = new Queue<float>();

        public bool Update(double deltaTime)
        {
            CurrentFramesPreSecond = 1.0f / (float)deltaTime;

            _sampleBuffer.Enqueue(CurrentFramesPreSecond);

            if(_sampleBuffer.Count > MAXINUM_SAMPLES)
            {
                _sampleBuffer.Dequeue();
                AverageFramesPreSecond = _sampleBuffer.Average(i => i);
            }
            else
            {
                AverageFramesPreSecond = CurrentFramesPreSecond;
            }

            TotalFrames++;
            TotalSeconds += (float)deltaTime;

            return true;
        }
    }
}
