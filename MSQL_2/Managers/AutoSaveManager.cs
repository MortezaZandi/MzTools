using System;
using System.Windows.Forms;

public class AutoSaveManager : IDisposable
{
    private readonly System.Windows.Forms.Timer _timer;
    private readonly Action _saveAction;
    private bool _isEnabled;
    
    public AutoSaveManager(Action saveAction)
    {
        _saveAction = saveAction;
        _timer = new System.Windows.Forms.Timer
        {
            Interval = SettingsManager.Current.AutoSaveIntervalMinutes * 60 * 1000
        };
        _timer.Tick += Timer_Tick;
        
        IsEnabled = SettingsManager.Current.AutoSaveEnabled;
    }
    
    public bool IsEnabled
    {
        get => _isEnabled;
        set
        {
            _isEnabled = value;
            if (_isEnabled)
            {
                _timer.Start();
            }
            else
            {
                _timer.Stop();
            }
        }
    }
    
    private void Timer_Tick(object sender, EventArgs e)
    {
        _saveAction?.Invoke();
    }
    
    public void Dispose()
    {
        _timer?.Dispose();
    }
} 