using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Mvvm;

namespace TaskApp.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly Dispatcher _dispatcher;
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        [ObservableProperty]
        private ObservableCollection<string> _items = new ObservableCollection<string>();

        public MainWindowViewModel(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        private bool _canScan = false;

        [RelayCommand]
        private async Task RunTaskAsync()
        {
            _canScan = false;
            Items.Clear();

            Items.Add($"任务即将开始 at {DateTime.Now}");
            var task1 = ScanCodeAsync();
            var task2 = RunAsync();
            Items.Add($"等待任务结束 at {DateTime.Now}");

            await Task.WhenAll(task1, task2);
            //.ContinueWith(x =>
            //{
            //    System.Diagnostics.Debug.WriteLine($"WhenAll ContinueWith {DateTime.Now}");
            //});
            Items.Add($"任务结束，执行以下步骤 at {DateTime.Now}");
            //System.Diagnostics.Debug.WriteLine($"任务结束，执行以下步骤 at {DateTime.Now}");
            await Task.Delay(1000);
            Items.Add($"后续工作执行结束 at {DateTime.Now}");
            System.Diagnostics.Debug.WriteLine($"后续工作执行结束 at {DateTime.Now}");

            Items.Add($"正常结束了 in {nameof(RunTaskCommand)} at {DateTime.Now}");
        }

        private async Task RunAsync()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    //System.Diagnostics.Debug.WriteLine($"while循环开始 at {DateTime.Now}");
                    await _dispatcher.InvokeAsync(() =>
                    {
                        Items.Add($"while循环开始 in {nameof(RunAsync)} at {DateTime.Now}");
                    });
                    await Task.Delay(4000);
                    //System.Diagnostics.Debug.WriteLine($"while循环 at {DateTime.Now}");
                    await _dispatcher.InvokeAsync(() =>
                    {
                        Items.Add($"while循环 in {nameof(RunAsync)} at {DateTime.Now}");
                    });
                    _canScan = true;
                    await Task.Delay(1500);
                    break;
                }

                await _dispatcher.InvokeAsync(() =>
                {
                    Items.Add($"while循环结束 in {nameof(RunAsync)} at {DateTime.Now}");
                });
                //System.Diagnostics.Debug.WriteLine($"while循环结束 at {DateTime.Now}");
            });
        }

        private async Task ScanCodeAsync()
        {
            await Task.Run(async () =>
            {
                while (!_canScan)
                {
                    await Task.Delay(200);
                    await _dispatcher.InvokeAsync(() =>
                    {
                        Items.Add(
                            $"等待扫码... in {nameof(ScanCodeAsync)} at {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}"
                        );
                    });
                    //System.Diagnostics.Debug.WriteLine($"等待扫码... at {DateTime.Now}");
                }

                await _dispatcher.InvokeAsync(() =>
                {
                    Items.Add($"开始扫侧码 in {nameof(ScanCodeAsync)} at {DateTime.Now}");
                });

                //System.Diagnostics.Debug.WriteLine($"开始扫侧码 at {DateTime.Now}");
                await Task.Delay(1000);
                await _dispatcher.InvokeAsync(() =>
                {
                    Items.Add($"扫侧码结束 in {nameof(ScanCodeAsync)} at {DateTime.Now}");
                });
                //System.Diagnostics.Debug.WriteLine($"扫侧码结束 at {DateTime.Now}");
                await Task.Delay(1000);
                await _dispatcher.InvokeAsync(() =>
                {
                    Items.Add($"开始扫底码 in {nameof(ScanCodeAsync)} at {DateTime.Now}");
                });
                //System.Diagnostics.Debug.WriteLine($"开始扫底码 at {DateTime.Now}");
                await Task.Delay(1000);
                await _dispatcher.InvokeAsync(() =>
                {
                    Items.Add($"扫底码结束 in {nameof(ScanCodeAsync)} at {DateTime.Now}");
                });
                //System.Diagnostics.Debug.WriteLine($"扫底码结束 at {DateTime.Now}");
            });
        }
    }
}
