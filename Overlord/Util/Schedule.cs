using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Overlord.Util
{

	public delegate void Job(params string[] args);

	public static class Schedule
	{

        private static Timer timer;

		public static void ScheduleJob(Job job, DateTime time, params string[] args)
		{
            if (job == null) throw new ArgumentNullException("job", "Job to run cannot be null");
            if (job == null) throw new ArgumentNullException("time", "Time to run a job cannot be null");

            DateTime current = DateTime.Now;
            TimeSpan timeToGo = time - current;

            if (timeToGo < TimeSpan.Zero)
            {
                job(args);
                return;
            }

            timer = new Timer(x =>
            {
                job(args);
            }, null, timeToGo, Timeout.InfiniteTimeSpan);
        }

	}
}
