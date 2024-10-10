using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Mvvm;

namespace TaskApp.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel() { }

        private bool _canScan = false;

        [RelayCommand]
        private async Task RunTaskAsync()
        {
            _canScan = false;
            var task1 = ScanCodeAsync();
            var task2 = RunAsync();
            System.Diagnostics.Debug.WriteLine($"等待任务结束 at {DateTime.Now}");
            Task.WaitAll(task1, task2);
            System.Diagnostics.Debug.WriteLine($"任务结束，执行以下步骤 at {DateTime.Now}");
            await Task.Delay(1000);
            System.Diagnostics.Debug.WriteLine($"后续工作执行结束 at {DateTime.Now}");
        }

        private async Task RunAsync()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    System.Diagnostics.Debug.WriteLine($"while循环开始 at {DateTime.Now}");
                    await Task.Delay(4000);
                    System.Diagnostics.Debug.WriteLine($"while循环 at {DateTime.Now}");
                    _canScan = true;
                    await Task.Delay(1500);
                    break;
                }
                System.Diagnostics.Debug.WriteLine($"while循环结束 at {DateTime.Now}");

                System.Diagnostics.Debug.WriteLine("Task 结束了");
            });
        }

        private async Task ScanCodeAsync()
        {
            await Task.Run(async () =>
            {
                while (!_canScan)
                {
                    await Task.Delay(200);
                    System.Diagnostics.Debug.WriteLine($"等待扫码... at {DateTime.Now}");
                }

                System.Diagnostics.Debug.WriteLine($"开始扫侧码 at {DateTime.Now}");
                await Task.Delay(1000);
                System.Diagnostics.Debug.WriteLine($"扫侧码结束 at {DateTime.Now}");
                await Task.Delay(1000);
                System.Diagnostics.Debug.WriteLine($"开始扫底码 at {DateTime.Now}");
                await Task.Delay(1000);
                System.Diagnostics.Debug.WriteLine($"扫底码结束 at {DateTime.Now}");
            });
        }
    }
}
