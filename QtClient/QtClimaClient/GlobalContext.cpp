#include "GlobalContext.h"

GlobalContext::GlobalContext(QObject *parent) : QObject(parent)
{

}

int GlobalContext::CurrentDay = 0;
int GlobalContext::CurrentHeads = 0;
int GlobalContext::TotalFanPerformance = 0;
