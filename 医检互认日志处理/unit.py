
from  collections import  deque

class FixedSizeDeque:
    def __init__(self,max_size=3):
        self.deque = deque(maxlen=max_size)

    def add(self,item):
        self.deque.append(item)

    def get_all(self):
        return list(self.deque)