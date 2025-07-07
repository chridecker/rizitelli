using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Rizitelli.App.Services
{
    public class VibrationHelper
    {
        public void Vibrate(TimeSpan duration)
        {
            try
            {
                Vibration.Default.Vibrate(duration);
            }
            catch { }
        }

        public Task Vibrate(int repeat, TimeSpan duration, CancellationToken cancellationToken) => this.Vibrate(repeat, duration, duration, cancellationToken);

        public async Task Vibrate(int repeat, TimeSpan duration, TimeSpan breakDuration, CancellationToken cancellationToken)
        {
            for (int i = 0; i < repeat; i++)
            {
                this.Vibrate(duration);
                await Task.Delay(duration, cancellationToken);
            }
        }

    }
}
