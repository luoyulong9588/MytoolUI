using System;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace MytoolMiniWPF.views
{
    /// <summary>
    /// 这部分是用于种子生产界面的后端代码
    /// </summary>
    public partial class Settings
    {

        public ObservableCollection<DataItem> DataItems { get; set; } = new ObservableCollection<DataItem>();
        private async void LoadData()
        {

            string connectionString = "Data Source=.\\config\\data.db"; // 连接字符串，指向你的数据库文件  
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand("SELECT * FROM diagnose", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var dataItem = new DataItem
                        {
                            Id = reader[0].ToString(),
                            Key = reader[1].ToString(),
                            Chief1 = reader[2].ToString(),
                            Chief2 = reader[3].ToString(),
                            Chief3 = reader[4].ToString(),
                            Chief4 = reader[5].ToString(),
                            Chief5 = reader[6].ToString(),
                            Chief6 = reader[7].ToString(),
                            Chief7 = reader[8].ToString(),
                            Chief8 = reader[9].ToString(),
                            Chief9 = reader[10].ToString(),
                            Chief10 = reader[11].ToString()
                        };
                        // 将chief1至chief10作为子节点添加到key节点下  
                        TreeViewItem keyItem = new TreeViewItem { Header = dataItem.Key };
                        keyItem.Tag = dataItem.Id;
                        keyItem.Items.Add(new TreeViewItem { Header = dataItem.Chief1 });
                        keyItem.Items.Add(new TreeViewItem { Header = dataItem.Chief2 });
                        keyItem.Items.Add(new TreeViewItem { Header = dataItem.Chief3 });
                        keyItem.Items.Add(new TreeViewItem { Header = dataItem.Chief4 });
                        keyItem.Items.Add(new TreeViewItem { Header = dataItem.Chief5 });
                        keyItem.Items.Add(new TreeViewItem { Header = dataItem.Chief6 });
                        keyItem.Items.Add(new TreeViewItem { Header = dataItem.Chief7 });
                        keyItem.Items.Add(new TreeViewItem { Header = dataItem.Chief8 });
                        keyItem.Items.Add(new TreeViewItem { Header = dataItem.Chief9 });
                        keyItem.Items.Add(new TreeViewItem { Header = dataItem.Chief10 });
                        // 将keyItem添加到TreeView中  
                        seedTreeView.Items.Add(keyItem);
                        DataItems.Add(dataItem);
                    }
                }
            }
        }
        private void seedTreeView_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            // 创建一个新的ContextMenu  
            ContextMenu contextMenu = new ContextMenu();

            if (GetParentNodeIndex() > -1) //如果选中的是父节点，增加一个新增的函数;
            {
                // 创建一个MenuItem用于新增操作  
                MenuItem addItem = new MenuItem();
                addItem.Header = "新增种子";
                addItem.Command = new RelayCommand(AddItem);
                contextMenu.Items.Add(addItem);
            }


            // 创建一个MenuItem用于编辑操作  
            MenuItem editItem = new MenuItem();
            editItem.Header = "编辑";
            editItem.Command = new RelayCommand(EditItem);

            // 将MenuItem添加到ContextMenu  
            contextMenu.Items.Add(editItem);
            // 将ContextMenu赋值给TreeView的ContextMenu属性  
            seedTreeView.ContextMenu = contextMenu;
        }

        /// <summary>
        /// 编辑节点
        /// </summary>
        private void EditItem()
        {
            // 获取选中的节点  
            TreeViewItem selectedItem = seedTreeView.SelectedItem as TreeViewItem;

            if (selectedItem == null) { return; }
            InputBox input = new InputBox(selectedItem.Header as string);
            input.ShowDialog();
            string newContent = input.Message;

            //("编辑节点内容", "编辑", currentContent, 500, 500);

            if (newContent == null || !input.Update) { return; }

            selectedItem.Header = newContent;

            // 遍历所有项并更新数据库  
            foreach (var item in seedTreeView.Items)
            {
                UpdateData(item as TreeViewItem);
            }
        }
        private void AddItem()
        {
            InputBox input = new InputBox("输入新增的种子");
            input.ShowDialog();
            if (!input.Update)
            {
                return;
            }
            // 将输入值添加到 TreeView 的节点中  
            //TreeViewItem parentItem = seedTreeView.Items[0] as TreeViewItem;
            TreeViewItem newItem = new TreeViewItem { Header = input.Message };
            newItem.Tag = (GetParentNodeIndex() + 1).ToString();
            newItem.Items.Add(new TreeViewItem { Header = "" });
            newItem.Items.Add(new TreeViewItem { Header = "" });
            newItem.Items.Add(new TreeViewItem { Header = "" });
            newItem.Items.Add(new TreeViewItem { Header = "" });
            newItem.Items.Add(new TreeViewItem { Header = "" });
            newItem.Items.Add(new TreeViewItem { Header = "" });
            newItem.Items.Add(new TreeViewItem { Header = "" });
            newItem.Items.Add(new TreeViewItem { Header = "" });
            newItem.Items.Add(new TreeViewItem { Header = "" });
            newItem.Items.Add(new TreeViewItem { Header = "" });
            seedTreeView.Items.Add(newItem);
            UpdateData(newItem);
        }

        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <param name="treeViewItem"></param>
        private async void UpdateData(TreeViewItem treeViewItem)
        {
            var itemLists = new List<string>();

            foreach (var item in treeViewItem.Items)
            {
                var s = item as TreeViewItem;
                var value = s.Header as string;
                itemLists.Add(value);
            }
            string connectionString = "Data Source=.\\config\\data.db";

            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SQLiteCommand("UPDATE diagnose SET Key=@Key, Chief1 = @Chief1, Chief2=@Chief2,Chief3=@Chief3,Chief4=@Chief4,Chief5=@Chief5,Chief6=@Chief6,Chief7=@Chief7,Chief8=@Chief8, Chief9=@Chief9,Chief10 = @Chief10 WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Key", treeViewItem.Header.ToString());
                    command.Parameters.AddWithValue("@Chief1", itemLists[0]);
                    command.Parameters.AddWithValue("@Chief2", itemLists[1]);
                    command.Parameters.AddWithValue("@Chief3", itemLists[2]);
                    command.Parameters.AddWithValue("@Chief4", itemLists[3]);
                    command.Parameters.AddWithValue("@Chief5", itemLists[4]);
                    command.Parameters.AddWithValue("@Chief6", itemLists[5]);
                    command.Parameters.AddWithValue("@Chief7", itemLists[6]);
                    command.Parameters.AddWithValue("@Chief8", itemLists[7]);
                    command.Parameters.AddWithValue("@Chief9", itemLists[8]);
                    command.Parameters.AddWithValue("@Chief10", itemLists[9]);
                    command.Parameters.AddWithValue("@Id", treeViewItem.Tag);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        private int GetParentNodeIndex()
        {

            int selectedIndex = -1;
            TreeViewItem selectedItem = seedTreeView.SelectedItem as TreeViewItem;

            if (selectedItem != null)
            {
                for (int i = 0; i < seedTreeView.Items.Count; i++)
                {
                    TreeViewItem item = seedTreeView.Items[i] as TreeViewItem;

                    if (item != null && item.IsSelected)
                    {
                        selectedIndex = i;
                        break;
                    }
                }
            }

            return selectedIndex;

        }

        private void seedTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // 更新ContextMenu  
            seedTreeView_ContextMenuOpening(sender, null);
        }

    }
}
