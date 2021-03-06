namespace Gu.Wpf.DataGrid2D
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    internal sealed class RowsListener : IDisposable
    {
        private readonly DataGrid dataGrid;
        private bool disposed;

        internal RowsListener(DataGrid dataGrid)
        {
            this.dataGrid = dataGrid;
            ////dataGrid.ItemContainerGenerator.ItemsChanged += this.OnItemsChanged;
            dataGrid.ItemContainerGenerator.StatusChanged += this.OnStatusChanged;
        }

        public void Dispose()
        {
            if (this.disposed)
            {
                return;
            }

            this.disposed = true;
            ////this.dataGrid.ItemContainerGenerator.ItemsChanged -= this.OnItemsChanged;
            this.dataGrid.ItemContainerGenerator.StatusChanged -= this.OnStatusChanged;
        }

        ////private void OnItemsChanged(object sender, ItemsChangedEventArgs e)
        ////{
        ////    this.dataGrid.RaiseEvent(RowsChangedEventArgs);
        ////}

        private void OnStatusChanged(object o, EventArgs e)
        {
            var generator = (ItemContainerGenerator)o;
            if (generator.Status == GeneratorStatus.ContainersGenerated)
            {
                this.dataGrid.RaiseEvent(new RoutedEventArgs(Events.RowsChangedEvent));
            }
        }
    }
}
