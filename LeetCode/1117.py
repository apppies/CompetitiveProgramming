# https://leetcode.com/problems/building-h2o/
class H2O:
    def __init__(self):
        self.sem_o = threading.Semaphore(1)
        self.sem_h = threading.Semaphore(0)
        self.cnt_h = 0
        self.mutex_h = threading.Lock()



    def hydrogen(self, releaseHydrogen: 'Callable[[], None]') -> None:
        self.sem_h.acquire(1)

        # releaseHydrogen() outputs "H". Do not change or remove this line.
        releaseHydrogen()

        self.mutex_h.acquire()
        
        self.cnt_h = self.cnt_h + 1
        if self.cnt_h % 2 == 0:
            self.sem_o.release(1)

        self.mutex_h.release()

        


    def oxygen(self, releaseOxygen: 'Callable[[], None]') -> None:

        self.sem_o.acquire(1)
        
        # releaseOxygen() outputs "O". Do not change or remove this line.
        releaseOxygen()

        self.sem_h.release(2)