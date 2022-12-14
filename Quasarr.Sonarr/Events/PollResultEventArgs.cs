// LFInteractive LLC. - All Rights Reserved

namespace Quasarr.Sonarr.Events;

public class PollResultEventArgs : EventArgs
{
    public int Current { get; set; }
    public double Percentage { get; set; }
    public string Status { get; set; }
    public int Total { get; set; }
}