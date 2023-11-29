using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace VisibilityTest;

internal class WindowViewModel : INotifyPropertyChanged
{
    public WindowViewModel()
    {
        ToggleButtonCommand = new ToggleVisibilityCommand(this);
        OpenWindowCommand = new OpenWindowCommand();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private Visibility _buttonVisibility = Visibility.Hidden;

    public Visibility ButtonVisibility
    {
        get
        {
            return _buttonVisibility;
        }
        set
        {
            _buttonVisibility = value;
            OnPropertyChanged("ButtonVisibility");
        }
    }

    public ICommand ToggleButtonCommand { get; }
    public ICommand OpenWindowCommand { get; }

    protected virtual void OnPropertyChanged(/*[CallerMemberName]*/ string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

internal class ToggleVisibilityCommand : ICommand
{
    private readonly WindowViewModel _viewModel;

    public event EventHandler? CanExecuteChanged;

    public ToggleVisibilityCommand(WindowViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        // Toggle button visibility
        _viewModel.ButtonVisibility = _viewModel.ButtonVisibility switch
        {
            Visibility.Hidden => Visibility.Visible,
            Visibility.Visible => Visibility.Hidden,
            _ => Visibility.Hidden
        };
    }
}

internal class OpenWindowCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        var window = new MainWindow();
        window.Show();
    }
}