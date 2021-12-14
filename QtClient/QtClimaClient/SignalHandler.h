#pragma once

#include <QObject>
#include <signal.h>

class SignalHandler
{
public:
    SignalHandler();
    static void init();
    static void handler(int sig, siginfo_t *dont_care, void *dont_care_either);
private:

    static struct sigaction sa;
};

