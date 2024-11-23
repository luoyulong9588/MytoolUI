import sqlite3
from enum import Enum

db_path = "D:\MytoolDataFiles\data\patient_infomations.db"




def query_from_local_db(string_idcard:str,query_type):
    diagnose = ""
    conn = sqlite3.connect(db_path)
    cur = conn.cursor()
    cur.execute("SELECT * FROM in_hospital_infos where id_card = ?", (string_idcard,)   )
    result = cur.fetchone()
    if result:
        diagnose  =result[query_type.value]
    conn.close()
    return diagnose

class QueryType(Enum):
    ADDRESS=7
    DIAGNOSE = 11
    PHONE = 5



if __name__ == '__main__':
    # resp= query_from_local_db("510223197604262616",QueryType.ADDRESS)
    # print(resp)
    conn = sqlite3.connect(r'D:\MytoolDataFiles\data\recognition_data.db')
    cur = conn.cursor()
    start_datetime = '2024-04-21 00:00:00'
    end_datetime = '2024-09-21 23:59:59'
    # cur.execute("SELECT * FROM recognition where log_date between ? and ?", (start_datetime,end_datetime,))
    cur.execute("SELECT * FROM recognition where address is '' ")
    result = cur.fetchall()
    print(result)