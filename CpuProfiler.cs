using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace MacOsSystemProfiler
{
    public class CpuProfiler : IDisposable
    {
        CancellationTokenSource source;
        CancellationToken cancelWait;
        (decimal, decimal, decimal)[] measurements = new (decimal, decimal, decimal)[2];
        int currentIndex = 0;
        private bool disposedValue;
        private readonly System.Timers.Timer timer;

        public CpuProfiler()
        {
            timer = new System.Timers.Timer(1);
            timer.Elapsed += new ElapsedEventHandler(OnElapsed);
            timer.AutoReset = false;

            source = new CancellationTokenSource();
            cancelWait = source.Token;

            timer.Start();
        }

        public Decimal UserPercentage => measurements[currentIndex].Item1;
        public Decimal SystemPercentage => measurements[currentIndex].Item2;
        public Decimal IdlePercentage => measurements[currentIndex].Item3;

        public bool Ready { get; private set; }

        public double WaitInterval => timer.Interval;

        public void WaitForReady()
        {
            if (!Ready)
                WaitForNextSample();
        }

        public void WaitForNextSample()
        {
            source = new CancellationTokenSource();
            cancelWait = source.Token;

            cancelWait.WaitHandle.WaitOne();
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            GetNextSampleAsync().Wait();

            if (timer.Interval == 1)
                timer.Interval = 500;

            timer.Start(); // Restart timer
        }

        private async Task GetNextSampleAsync()
        {
            using Process p = new()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "/bin/bash",
                    Arguments = "-c \"top -l1 | grep -E 'CPU usage:' |  awk -v FS='CPU usage: | user, | sys, | idle' '{print $2, $3, $4}'\"",
                    RedirectStandardOutput = true
                }
            };
            _ = p.Start();
            p.WaitForExit();
            var output = await p.StandardOutput.ReadToEndAsync();
            var statParts = output.Split(' ');
            var stats = (from s in statParts
                         select decimal.Parse(s.Substring(0,s.IndexOf('%')))).ToArray();

            int newIndex;
            if (currentIndex == 0)
                newIndex = 1;
            else
                newIndex = 0;

            measurements[newIndex] = (stats[0], stats[1], stats[2]);
            currentIndex = newIndex;

            Ready = true;
            source.Cancel();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    timer.Dispose();
                }

                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~CpuProfiler()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
