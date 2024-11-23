
import pandas as pd
import time
import os
from pathlib import Path
from pyecharts.render.display import Javascript
from pyecharts import options as opts
from pyecharts.charts import Bar,Page,Pie,Grid,Radar, Boxplot, Scatter
from pyecharts.commons.utils import JsCode
from pyecharts.globals import ThemeType,CurrentConfig
from pyecharts.options import AxisOpts,LabelOpts
from rich import print
# from rich import print
# 读取整个xls文件的所有sheet（默认读取第一个sheet）

CurrentConfig.ONLINE_HOST = str(os.getcwd()) + "\\assets\\v5\\"


def get_index(search_list,search_str):
    user_info = {}
    index = None
    try:
        index = search_list.index(search_str)
    except:
        index = -1
    return {"name":search_str,"index":index}


# 定义新的load_javascript_contents函数
def new_js(self):
    for lib in self.lib:
        work_path = str(Path.cwd()) + "/pyecharts-assets-local-scripts/assets/v5/"
        new_lib = lib.replace("https://assets.pyecharts.org/assets/v5/", f"{work_path}")
        with open(new_lib, "r", encoding="utf-8") as f:
            self.javascript_contents[lib] = f.read()
            f.close()
        continue
    return self

class  BuildPages:

    def __init__(self,file_1,file_2):
        self.file_1 = file_1
        self.file_2 = file_2
        self.is_analysis = True if len(file_1)>0 else False
        self.is_avg = True if len(file_2)>0 else False
        self.load_data()



    def load_data(self):

        self.data1 = pd.read_excel(self.file_1, skiprows=2, skipfooter=3)
        self.data1.drop(self.data1.columns[0], axis=1, inplace=True)
        self.header_row = list(self.data1.iloc[0])

        new_columns = self.data1.iloc[0]
        self.data1.columns = self.data1.iloc[0]

        self.data1 = self.data1.drop(self.data1.index[0])
        self.data1["copy_column"] = self.data1["开单人"].copy()
        
        self.data1.set_index(self.data1["copy_column"],inplace = True)
        del self.data1["copy_column"]

        self.lists_arry = []
        for column in new_columns:
            self.lists_arry.append([column] +list(self.data1[column]))

        self.user_name_list = list(self.data1["开单人"])
    

        # "合计终为最后一行"

        self.user_data_for_stack = {}
        for index,name in enumerate(self.user_name_list[:-1]):
            user_data = []
            for column in new_columns[1:-1]:
                value = {"value": self.data1.loc[name,column], "percent": float(self.data1.loc[name,column]) / self.data1[column].iloc[-1]}
                user_data.append(value)
            self.user_data_for_stack[name] = user_data

        if self.is_avg:
            self.data2 = pd.read_excel(self.file_2, skiprows=2, skipfooter=1)
            self.data2.drop(self.data2.columns[0], axis=1, inplace=True)
            self.avg_days = []
            name_list = set(self.data2["主治医生"])
            self.scatter_list = []
            for name in name_list:
                user_days_collect = {"name":name,"value":self.data2[self.data2['主治医生'] == name]['天数'].tolist()}
                self.scatter_list.append(user_days_collect)


            self.days_lyl = {"name":"罗玉龙","value":self.data2[self.data2['主治医生'] == '罗玉龙']['天数'].tolist()}
            self.days_pyh = {"name":"彭育欢","value":self.data2[self.data2['主治医生'] == '彭育欢']['天数'].tolist()}
            self.days_zqx = {"name":"朱庆霞","value":self.data2[self.data2['主治医生'] == '朱庆霞']['天数'].tolist()}
            self.days_wxl = {"name":"王雪玲","value":self.data2[self.data2['主治医生'] == '王雪玲']['天数'].tolist()}
            self.days_lyh = {"name":"刘益宏","value":self.data2[self.data2['主治医生'] == '刘益宏']['天数'].tolist()}
            self.days_fr = {"name":"付饶","value":self.data2[self.data2['主治医生'] == '付饶']['天数'].tolist()}


    def bar_stack(self):
        c = (
            Bar(init_opts=opts.InitOpts(theme=ThemeType.LIGHT,width='2000px',height='1000px',))
            .add_xaxis(self.header_row[1:-1])
            .set_global_opts(
            title_opts=opts.TitleOpts(title="收入占比总览",pos_left='10%'),
            xaxis_opts=AxisOpts(
            axislabel_opts=LabelOpts(
            rotate=45,  # 设置标签旋转45度
            font_size=12  # 设置字体大小为12
                )
                )

                )

        .set_series_opts(
                label_opts=opts.LabelOpts(
                    position="right",
                    formatter=JsCode(
                        "function(x){return Number(x.data.percent * 100).toFixed() + '%';}"
                    ),
                    font_size=10,  # 设置字体大小
                )
            )
        )
       
        for name in self.user_data_for_stack:
            c.add_yaxis(name,self.user_data_for_stack[name], label_opts = opts.LabelOpts(is_show = False),stack="stack3", category_gap="20%")
        return c

    def bar_avg(self):
        days_list = [self.days_lyl,self.days_pyh,self.days_zqx,self.days_wxl,self.days_lyh,self.days_fr]
        x_data = [i['name'] for i in self.scatter_list if len(i['value'])>0]
        c = (
            Bar()
            .add_xaxis(x_data)
            .add_yaxis("CNY",[round(self.data1.loc[i['name'],'合计']/len(i['value']),2) for i in self.scatter_list if len(i['value'])>0])
            #.add_yaxis("CNY",[self.list_39[self.user_name_list.index(i['name'])]/len(i['value']) for i in days_list if len(i['value'])>0],
            #           )

            .set_global_opts(
                title_opts=opts.TitleOpts(title="平均住院费用", subtitle="如果周期选择错误，该项无参考意义",pos_left='10%'),
                brush_opts=opts.BrushOpts(),
            )
            # .render("bar_with_brush.html")
        )
        return  c

    def base_pie(self,column_list):
        print(self.user_name_list[0:-1])  
        print(list(zip(self.user_name_list[0:-1], column_list[1:-1])))
        # time.sleep(999)

        c = (
            Pie(init_opts=opts.InitOpts(theme=ThemeType.LIGHT,width='800px'))
            .add("", [list(z) for z in zip(self.user_name_list[0:-1], column_list[1:-1])],
                 radius=["40%", "55%"],
                 )
            .set_global_opts(title_opts=opts.TitleOpts(title=f"{column_list[0]}占比 ",pos_left='5%',subtitle=f"[单项收入:总收入(CYN) - 单项占比 {column_list[-1]} : {self.data1.loc['合计','合计']} - {column_list[-1]*100/self.data1.loc['合计','合计']:.2f}%]"),legend_opts=opts.LegendOpts(pos_left="2%", pos_top="25%",orient='vertical',))
            .set_series_opts(label_opts=opts.LabelOpts(formatter="{b}: {c} ({d}%)"))
        )
        return c

    def radar(self):
        radar_data_list = []
        color_list = ["red","orange","#458B00","#FF37F9","#7D26CD","#9D23CD","#8D43CD","grey"]
        name_list = []
        for name in self.user_name_list:
            if name in "接口用合计罗玉文周俊邓先桂":
                continue
            radar_data =[[
                self.data1.loc[name,"普通治疗费"],
                self.data1.loc[name,"监测费"],
                self.data1.loc[name,"远程动态血压监测"],
                self.data1.loc[name,"胃镜费"] +  self.data1.loc[name,"肠镜费"],
               self.data1.loc[name,"输氧费"],
               self.data1.loc[name,"动态心电图"],
               self.data1.loc[name,"CT费"],
               self.data1.loc[name,"彩超费"]
                ]]
            radar_data_list.append(radar_data)
            name_list.append(name)
        c =(
            Radar(init_opts=opts.InitOpts())
            .add_schema(
                schema=[
                    opts.RadarIndicatorItem(name="治疗费", max_=5000),
                    opts.RadarIndicatorItem(name="心电监护", max_=30000),
                    opts.RadarIndicatorItem(name="动态血压", max_=2000),
                    opts.RadarIndicatorItem(name="胃肠镜", max_=4000),
                    opts.RadarIndicatorItem(name="吸氧", max_=10000),
                    opts.RadarIndicatorItem(name="动态心电图", max_=5000),
                    opts.RadarIndicatorItem(name="CT", max_=20000),
                    opts.RadarIndicatorItem(name="彩超", max_=25000),
                ],
                splitarea_opt=opts.SplitAreaOpts(
                    is_show=True, areastyle_opts=opts.AreaStyleOpts(opacity=1)
                )
            )
            .set_series_opts(label_opts=opts.LabelOpts(is_show=False))
            .set_global_opts(
                title_opts=opts.TitleOpts(title="主要指标",pos_left='10%'), legend_opts=opts.LegendOpts(pos_left="10%", pos_top="25%",orient='vertical'),
            )
        )
        for index,name in enumerate(name_list):
            print(index,name)
            c.add(series_name = name,data = radar_data_list[index],color = color_list[index], label_opts = opts.LabelOpts(is_show = False))
        return c

    def scatter(self):
        x_data = [i['name'] for i in self.scatter_list if len(i['value'])>0]

        y_data =  [i['value'] for i in self.scatter_list if len(i['value'])>0]
        print(x_data)
        print("---------")
        print(y_data)
        box_plot = Boxplot()

        box_plot = (
            box_plot.add_xaxis(xaxis_data=x_data)
            .add_yaxis(series_name="", y_axis=box_plot.prepare_data(y_data),label_opts=opts.LabelOpts(is_show=False, position="top"))
            .set_global_opts(
                title_opts=opts.TitleOpts(
                pos_left="center", title="Hospital Stay Duration"
                ),
                tooltip_opts=opts.TooltipOpts(trigger="axis", axis_pointer_type="cross",formatter=lambda params: f"中位数: {params[0].data[1]}<br/>"  
                                 f"下四分位数 (Q1): {params[0].data[2]}<br/>"  
                                 f"上四分位数 (Q3): {params[0].data[4]}<br/>"  
                                 f"最大值: {params[0].data[-1]}<br/>"  
                                 f"最小值: {params[0].data[0]}"  ),

                xaxis_opts=opts.AxisOpts(
                    type_="category",
                    boundary_gap=True,
                    splitarea_opts=opts.SplitAreaOpts(is_show=False),
                    axislabel_opts=opts.LabelOpts(formatter="{value}"),
                    splitline_opts=opts.SplitLineOpts(is_show=False),
                ),
                yaxis_opts=opts.AxisOpts(
                    type_="value",
                    name="Days",
                    splitarea_opts=opts.SplitAreaOpts(
                    is_show=True, areastyle_opts=opts.AreaStyleOpts(opacity=1)

                    ),
                    max_=30
                ),
            )
        )

        scatter_data = [ round(sum(i['value'])/len(i['value']),2) for i in self.scatter_list if len(i['value'])>0]
        # return box_plot
        scatter = (
            Scatter()
            .add_xaxis(xaxis_data=x_data)
            .add_yaxis(series_name="", y_axis=scatter_data)
            .set_global_opts(
                title_opts=opts.TitleOpts(
                    pos_left="10%",
                    pos_top="90%",
                    title="upper: Q3 + 1.5 * IQR \nlower: Q1 - 1.5 * IQR",
                    title_textstyle_opts=opts.TextStyleOpts(
                        border_color="#999", border_width=1, font_size=12
                    ),

                ),
                yaxis_opts=opts.AxisOpts(
                    axislabel_opts=opts.LabelOpts(is_show=False),
                    axistick_opts=opts.AxisTickOpts(is_show=False),
                    max_=30
                ),
            )
        )

        grid = (
            Grid()
            .add(
                box_plot,
                grid_opts=opts.GridOpts(pos_left="10%", pos_right="10%", pos_bottom="15%"),
            )
            .add(
                scatter,
                grid_opts=opts.GridOpts(pos_left="10%", pos_right="10%", pos_bottom="15%"),
            )

        )
        return grid
    def run(self):
        page = Page(layout=Page.SimplePageLayout)
        if self.is_avg:
            page.add(self.scatter(),
            self.bar_avg())
        if self.is_analysis:
            page.add(
                self.bar_stack(),
                self.radar())
            for column_list in self.lists_arry[1:]:
                page.add(self.base_pie(column_list))
        page.render("page_draggable_layout.html")

if __name__ == "__main__":
    Javascript.load_javascript_contents = new_js
    app = BuildPages("1.xls","2.xls")
    app.run()

