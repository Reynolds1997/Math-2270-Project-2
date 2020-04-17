
from random import randint
import numpy
import time

class Problem2:
    #Constants
    RAIN_PERCENT = .4
    DOUBLE_SALE_BUSINESS_PERCENT = .5
    TOTAL_WEEKS = 1000.0
    ITER_AMOUNT = .05

    @staticmethod
    def setBusinessPercents(isRainy: bool):
        """[summary]
                returns sc_ns_bp, lc_ns_bp, ss_bp
        """
        if (isRainy):
            return (.2, .8, .1)
        else:
            return (.4, .6, .9)
    
    @staticmethod
    def eventOccurred(prob: float):
        percent = randint(0, 99)
        # print("Event Percent: "+ str(percent))
        return True if (percent < (prob * 100)) else False 

    @staticmethod
    def getBusinessPercentBySales(sc_hasSale: bool, lc_hasSale: bool, sc_ns_bp: float, lc_ns_bp:float, ss_bp:float):
        """[summary]
            returns sc_bp, lc_bp
        """
        if sc_hasSale and lc_hasSale:
            return Problem2.DOUBLE_SALE_BUSINESS_PERCENT, Problem2.DOUBLE_SALE_BUSINESS_PERCENT
        elif sc_hasSale:
            return ss_bp, 1-ss_bp
        elif lc_hasSale:
            return 1-ss_bp, ss_bp
        else:
            #print("neither had a sale")
            return sc_ns_bp, lc_ns_bp
    
    @staticmethod
    def getTotalBusinessPercentForSalePercents(sc_sp:float, lc_sp:float):
        """[summary]
            returns sc_tbp, lc_tbp, actualRainPercent
        """
        sc_tbp,lc_tbp,actualRainPercent = 0.0,0.0,0.0
        for i in range(int(Problem2.TOTAL_WEEKS)):
            isRainy = Problem2.eventOccurred(Problem2.RAIN_PERCENT)
            if isRainy:
                actualRainPercent += 1
            sc_hasSale = Problem2.eventOccurred(sc_sp)
            lc_hasSale = Problem2.eventOccurred(lc_sp)
            sc_ns_bp, lc_ns_bp, ss_bp = Problem2.setBusinessPercents(isRainy)
            sc_bp, lc_bp = Problem2.getBusinessPercentBySales(sc_hasSale, lc_hasSale, sc_ns_bp, lc_ns_bp, ss_bp)
            sc_tbp += sc_bp
            lc_tbp += lc_bp

        sc_tbp /= Problem2.TOTAL_WEEKS
        lc_tbp /= Problem2.TOTAL_WEEKS
        actualRainPercent /= Problem2.TOTAL_WEEKS
        return sc_tbp, lc_tbp, actualRainPercent


    @staticmethod
    def businessStaticArray(X_arr, Y_arr):
        pass
    
    @staticmethod
    def GetBestsString(name:str, bestForTuple):
        return (f"Best for {name}\n" 
        + f"    Small Center Sale Probability:{bestForTuple[0]:.3f}\n"
        + f"    Large Center Sale Probability:{bestForTuple[1]:.3f}\n"
        + f"    Small Center Business Share Percent:{bestForTuple[2]:.3f}\n"
        + f"    Large Center Business Share Percent:{bestForTuple[3]:.3f}\n"
        + f"    Actual Rain Percent (Expected is .4):{bestForTuple[4]:.3f}\n")
    
    @staticmethod
    def CalculateBestSalePercents():
        """
        return sc_sp array, lc_sp array, sc_tbp array
        """
        bestForSmallCenter = (0.0,0.0,0.0,0.0,0.0)
        bestForLargeCenter = (0.0,0.0,0.0,0.0,0.0)

        

        numRange = numpy.arange(0.0, 1, Problem2.ITER_AMOUNT)
        print(str(numRange))
        # time.sleep(2)
        print()
        X = []
        Y = []
        Z = []
        for lc_sp in numRange:
            for sc_sp in numRange:
                print(f"Trying large sale prob {lc_sp:.2f} and small sale prob {sc_sp:.2f}")
                sc_tbp, lc_tbp, actualRainPercent = Problem2.getTotalBusinessPercentForSalePercents(sc_sp, lc_sp)
                # print(f"{Problem2.GetBestsString('Item',(sc_sp, lc_sp, sc_tbp, lc_tbp, actualRainPercent))}")
                if (bestForSmallCenter[2] < sc_tbp):
                    bestForSmallCenter = (sc_sp, lc_sp, sc_tbp, lc_tbp, actualRainPercent)
                if (bestForLargeCenter[3] < lc_tbp):
                    bestForLargeCenter = (sc_sp, lc_sp, sc_tbp, lc_tbp, actualRainPercent)
                X.append(sc_sp)
                Y.append(lc_sp)
                Z.append(sc_tbp)
        resultStr = (f"\nFinal results:\nWeeks per trial: {Problem2.TOTAL_WEEKS}\n" 
        + f"{Problem2.GetBestsString('Small Center',bestForSmallCenter)}" 
        + f"{Problem2.GetBestsString('Large Center',bestForLargeCenter)}")
        print(resultStr)
        return X,Y,Z

if __name__ == "__main__":
    Problem2.CalculateBestSalePercents()