import sys
import webbrowser
import os
from PyQt5.QtWidgets import QApplication, QWidget, QHBoxLayout, QVBoxLayout, QPushButton, QLabel, QFileDialog
from PyQt5.QtCore import Qt, QRect,QDir
from PyQt5.QtGui import  QScreen,QFontDatabase,QGuiApplication
import pandas as pd
from pyecharts import options as opts
from pyecharts.charts import Bar,Page,Pie,Grid,Tab,Radar, Boxplot, Scatter
from pyecharts.commons.utils import JsCode
from pyecharts.globals import ThemeType
from pyecharts.faker import Faker
from pyecharts.options import AxisOpts,LabelOpts
from rich import print
from collect_data import *
class FileSelector(QWidget):
    def __init__(self):
        super().__init__()
        self.initUI()

    def initUI(self):
        self.setWindowTitle('文件选择器')

        # 设置窗口大小并居中
        screen_geometry = QApplication.primaryScreen().geometry()
        center_point = screen_geometry.center()
        self.move(center_point.x() - 300, center_point.y() - 150)  # 移动窗口到屏幕中央
        self.resize(600, 300)  # 设置窗口大小
        self.setStyleSheet("""  
            QPushButton {  
                background-color: orange; /* Green */  
                border: 2px solid gray;  
                border-radius: 10px;  /* 设置圆角大小 */  
                color: black;  
                padding: 5px 10px;  
                font-size: 16px; /* 修改字体大小为16像素 */  
                cursor: pointer;  
            }  
            QPushButton:hover {  
            background-color: darkOrange;    /* 鼠标悬停时变为红色 */  
            }  
            QLabel {  
                font-size: 16px; /* 修改字体大小为16像素 */  
                margin-right: 10px;  
                border: 1px solid gray;  
                border-radius: 5px;  /* 设置圆角大小 */  
                background-color: #E8E8E8;
                
            }  
        """)
        # 创建水平和垂直布局
        main_layout = QVBoxLayout()
        line1_layout = QHBoxLayout()
        line2_layout = QHBoxLayout()
        line3_layout = QHBoxLayout()



        # main_layout = QHBoxLayout()
        # left_layout = QVBoxLayout()
        # right_layout = QVBoxLayout()

        # 创建标签和按钮
        self.label1 = QLabel('区间报表：', self)
        self.label2 = QLabel('同期出院列表：', self)
        self.btn_select_1 = QPushButton('Browse', self)
        self.btn_select_2 = QPushButton('Browse', self)
        self.btn_start = QPushButton('Start', self)
        self.btn_exit = QPushButton('Quit',self)
        # 设置按钮大小
        self.btn_select_1.setFixedSize(140, 35)
        self.btn_select_2.setFixedSize(140, 35)
        self.btn_start.setFixedSize(140, 35)
        self.btn_exit.setFixedSize(140, 35)
        self.label1.setFixedHeight(35)  # 设置固定高度为 50 像素
        self.label2.setFixedHeight(35)  # 设置固定高度为 50 像素

        # button.setSizePolicy(QSizePolicy.Expanding, QSizePolicy.Fixed)
        # 添加控件到布局
        line1_layout.addWidget(self.label1)
        line1_layout.addWidget(self.btn_select_1)
        line2_layout.addWidget(self.label2)
        line2_layout.addWidget(self.btn_select_2)
        line3_layout.addWidget(self.btn_start)
        line3_layout.addWidget(self.btn_exit)

        # 将左右布局添加到主布局中
        main_layout.addLayout(line1_layout)  # 设置左侧布局的拉伸因子为1
        main_layout.addLayout(line2_layout)  # 设置右侧布局的拉伸因子为2

        main_layout.addLayout(line3_layout)
        self.setLayout(main_layout)
        # 连接信号与槽
        self.btn_select_1.clicked.connect(self.select_file_1)
        self.btn_select_2.clicked.connect(self.select_file_2)
        self.btn_start.clicked.connect(self.start)
        self.btn_exit.clicked.connect(self.exit)

    def exit(self):
        sys.exit(app.exec_())

    def select_file_1(self):
        fileName, _ = QFileDialog.getOpenFileName(self, "选择区间报表",QDir.homePath() + '/Desktop')
        if fileName:
            self.label1.setText(f'区间报表：{fileName}')

    def select_file_2(self):
        fileName, _ = QFileDialog.getOpenFileName(self, "选择同期入出院表",QDir.homePath() + '/Desktop')
        if fileName:
            self.label2.setText(f'同期入出院表：{fileName}')

    def check_file(self):
        if self.label1.text() != None and self.label2.text()!=None:
            return True
        else:
            return False

    def start(self):
        if self.label1.text() != None and len(self.label1.text())> 0:

            app = BuildPages(self.label1.text().split("：")[1], self.label2.text().split("：")[1])
            app.run()
            print("success!")


            # 获取当前工作目录
            current_dir = os.getcwd()

            # HTML文件的路径
            html_file_path = os.path.join(current_dir, 'page_draggable_layout.html')  # 替换为你的HTML文件路径

            # 使用默认浏览器打开HTML文件
            webbrowser.open('file://' + html_file_path)
        else:
            pass

if __name__ == '__main__':
    QApplication.setHighDpiScaleFactorRoundingPolicy(Qt.HighDpiScaleFactorRoundingPolicy.PassThrough)

    app = QApplication(sys.argv)
    ex = FileSelector()
    ex.show()
    sys.exit(app.exec_())
