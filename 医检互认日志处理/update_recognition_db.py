

import  sqlite3


def update_addr_phone():
    conn1 = sqlite3.connect(r'D:\MytoolDataFiles\data\recognition_data.db')
    conn2 = sqlite3.connect(r'D:\MytoolDataFiles\data\patient_infomations.db')
    cursor1 = conn1.cursor()
    cursor2 = conn2.cursor()
    cursor1.execute("""select patient_id from recognition where address is '' """)
    patient_with_out_address  = cursor1.fetchall()
    for patient in patient_with_out_address:
        _id_card = patient[0]
        cursor2.execute("""select addr_now, phone from in_hospital_infos where id_card = ?""",(_id_card,))
        addr_result = cursor2.fetchone()
        if addr_result:
            addr = addr_result[0]
            phone = addr_result[1]
            cursor1.execute("""update recognition set address=?,phone=? where patient_id = ?""",(addr,phone,_id_card))
            print(f"update address and phone for  {_id_card} into recognition_data.db ")
        else:
            print(f"no address and phone found for {_id_card} in patient_infomations.db")
    conn1.commit()
    conn1.close()
    conn2.close()


def update_diagnose():
    conn1 = sqlite3.connect(r'D:\MytoolDataFiles\data\recognition_data.db')
    conn2 = sqlite3.connect(r'D:\MytoolDataFiles\data\patient_infomations.db')
    cursor1 = conn1.cursor()
    cursor2 = conn2.cursor()
    cursor1.execute("""select patient_id from recognition where diagnose is '' """)
    patient_with_out_address  = cursor1.fetchall()
    for patient in patient_with_out_address:
        _id_card = patient[0]
        cursor2.execute("""select diagnose from in_hospital_infos where id_card = ?""",(_id_card,))
        diagnose_result = cursor2.fetchone()
        if diagnose_result:
            diagnose = diagnose_result[0]
            cursor1.execute("""update recognition set diagnose=? where patient_id = ?""",(diagnose,_id_card))
            print(f"update diagnose for  {_id_card} into recognition_data.db ")
        else:
            print(f"no diagnose found for {_id_card} in patient_infomations.db")
    conn1.commit()
    conn1.close()
    conn2.close()


def main():
    update_addr_phone()
    update_diagnose()

if __name__ == '__main__':
    main()