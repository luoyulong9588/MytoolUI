import  os

def filter_files(directory):
    items = os.listdir(directory)
    files =[]
    for item in items:
        full_path = os.path.join(directory, item)
        if os.path.isfile(full_path):
            files.append(full_path)
    for file in files:
        get_file_item(file)


def get_file_item(file_path):
    with open(file_path,'r',encoding='utf-8') as f:
        lines = f.readlines()

    for line in lines:
        print(line)
        if '"CT"' in line:
            print(file_path)
            break

if __name__ == '__main__':
    directory = "Log_医检互认"
    filter_files(directory)