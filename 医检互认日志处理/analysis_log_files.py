# This is a sample Python script.
import json
import os
from itertools import filterfalse
import sys
from rich import print
from datetime import datetime
import sqlite3
from unit import FixedSizeDeque
from query_from_local_db import query_from_local_db,QueryType

# Press Shift+F10 to execute it or replace it with your code.
# Press Double Shift to search everywhere for classes, files, tool windows, actions, and settings.



def calculate_age(birth_date_str: str):
    birth_date = datetime.strptime(birth_date_str, "%Y%m%d")
    today = datetime.today()
    print(today)
    print(birth_date)
    age = today.year - birth_date.year - ((today.month, today.day) < (birth_date.month, birth_date.day))
    return age


def get_file_item(file_path):
    with open(file_path, 'r', encoding='utf-8') as f:
        lines = f.readlines()
    patients_item = FixedSizeDeque()
    patients_list = []
    for line in lines:
        age = patient_name = patient_id = patient_birthday = patient_gender = patient_diagnose = patient_phone = patient_address = patient_name = patient_new_item = ""
        type_1 = False
        type_2 = False
        patients_item.add(line)
        if "要打开" in line:
            # print(file_path)
            patients_item_to_list = patients_item.get_all()
            entry_line = patients_item_to_list[0]
            leave_line = patients_item_to_list[1]
            log_date = entry_line.split("====>")[0]
            # print(entry_line)
            # print(leave_line)
            try:
                entry_info = json.loads(entry_line.split("入参:")[1])
                leave_info = json.loads(leave_line.split("出参:")[1])
            except:
                continue
            # print(leave_info)

            if "患者近期相似检查项目查询接口" in entry_line:
                type_1 = True
                print(entry_info)
                print(leave_info)
                patient_name = entry_info["patience"]["name"]
                patient_id = entry_info["patience"]["id_no"]
                patient_birthday = entry_info["patience"]["birthday"].replace("-","")
                patient_gender = "男" if entry_info['patience']['gender']== "1" else "女"
                patient_diagnose = query_from_local_db(patient_id,QueryType.DIAGNOSE)

                patient_phone = entry_info["patience"]["phone"]
                patient_address = entry_info["patience"]["address"]
                doctor_name = entry_info["doctor"]["name"]
                doctor_practice_number = entry_info["doctor"]["practice_number"]
                doctor_idcard = entry_info["doctor"]["idcard"]
                patient_new_item = [f'{item["project_name"]}:{item["study_body_part"]}'   for item in entry_info["visit_order"]["project_list"]]

                age = calculate_age(patient_birthday)
            if "您开立的项目有可互认的结果，请确认是否互认" in leave_line:
                type_2 = True
                # orign_info = json.loads(line.split("出参:")[1])
                patient_id = entry_info['messages']['PID']['identityNo']
                patient_name = entry_info['messages']['PID']['personalName']
                patient_gender = "男" if entry_info['messages']['PID']['sexCode'] == "1" else "女"
                patient_birthday = entry_info['messages']['PID']['birthDate']
                patient_diagnose = entry_info['messages']['DG1']['diagName']
                patient_new_item = entry_info['messages']['OBR']['lab'][0]["labItemName"]
                doctor_name = entry_info['messages']['PV1']['doctorName']
                patient_old_item = [i.split(' ')[0] for i in leave_info['data']['QAK']['brief']['entry']]
                # visit_date = entry_info['messages']['PV1']['visitDate']  # 这不是发生日期，发生日期还是得通过文件名。
                patient_phone = query_from_local_db(patient_id,QueryType.PHONE)
                patient_address = query_from_local_db(patient_id,QueryType.ADDRESS)
            if type_1 or type_2:
                # print(f"birthday:{patient_birthday}")
                age = calculate_age(patient_birthday)
                item_to_save = {
                            "patient_id": patient_id,
                            "name": patient_name,
                            "doctor": doctor_name,
                            "birthday": patient_birthday,
                            "gender": patient_gender,
                            "age": f"{age}岁",
                            "diagnose": patient_diagnose,
                            "patient_phone":patient_phone,
                            "patient_address":patient_address,
                            # "old_item": patient_old_item,
                            "new_item": patient_new_item,
                            # "visit_date": visit_date,
                            "log_date": log_date
                        }
                patients_list.append(item_to_save)
    print(patients_list)
    return patients_list


def filter_files(directory):
    items = os.listdir(directory)
    files = []
    for item in items:
        full_path = os.path.join(directory, item)
        if os.path.isfile(full_path):
            files.append(full_path)
    # print(files)
    print("files print end.")
    for file in files:
        patients_list = get_file_item(file)
        insert_db(patients_list)



def query_db(patient_item_to_save) -> bool:
    log_date = patient_item_to_save['log_date']
    sql = f"""select * from recognition where log_date = '{log_date}'"""
    result = cursor.execute(sql).fetchall()
    if len(result) < 1:
        return False
    return True


def insert_db(patient_item_to_save):
    sql = f"""insert into recognition values(?,?,?,?,?,?,?,?,?,?,?)"""
    if len(patient_item_to_save) < 1 or patient_item_to_save is None:
        return
    for item in patient_item_to_save:

        content = [item["log_date"], item["name"], item["patient_id"],
                   item["birthday"], item["gender"], item["age"],
                   item["diagnose"], item["patient_address"], "".join(item["new_item"]),
                   item["patient_phone"], item["doctor"]]
        if query_db(item):
            print("data already exists in recognition.")
            continue
        cursor.execute(sql, content)
        print("data insert success!")
    conn.commit()


def create_db():
    # 定义SQL语句来创建表
    create_table_sql = '''
    CREATE TABLE IF NOT EXISTS recognition (
        log_date TEXT PRIMARY KEY,
        name TEXT,
        patient_id TEXT,
        birthday TEXT,
        gender TEXT,
        age TEXT,
        diagnose TEXT,
        address TEXT,
        new_item TEXT,
        phone TEXT,
        doctor TEXT
    )
    '''
    conn.commit()
    # 执行SQL语句
    cursor.execute(create_table_sql)

    create_table_sql = '''
        CREATE TABLE IF NOT EXISTS zlbh_path (
            directory TEXT PRIMARY KEY
        )
        '''
    cursor.execute(create_table_sql)
    # 提交事务
    conn.commit()
    # 打印一个确认信息
    print("Table 'recognition' created successfully.")


# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    directory = "Log_医检互认"
    if len(sys.argv) > 1:
        directory = sys.argv[1]
        print(f"get cmd arg: {directory} !")
    else:
        print("no cmd arg!")

    conn = sqlite3.connect(r'D:\MytoolDataFiles\data\recognition_data.db')
    cursor = conn.cursor()
    create_db()
    filter_files(directory)
    conn.close()
#    · input(f"All log files in {directory} have been inserted into the database at D:\\MytoolDataFiles\\data\\recognition_data.db"
# )
