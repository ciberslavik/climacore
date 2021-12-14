#include "SignalHandler.h"
#include <execinfo.h>
#include "iostream"
struct sigaction SignalHandler::sa;
SignalHandler::SignalHandler()
{

}

void SignalHandler::init()
{
    memset(&sa, 0, sizeof(struct sigaction));
    sigemptyset(&sa.sa_mask);

    sa.sa_flags     = SA_NODEFER;
    sa.sa_sigaction = SignalHandler::handler;

    sigaction(SIGSEGV, &sa, NULL);
}

void SignalHandler::handler(int sig, siginfo_t *dont_care, void *dont_care_either)
{
    //std::cout << sig;
    int nptrs;
    void *buffer[100];
    char **strings;

    nptrs = backtrace(buffer,100);

    strings = backtrace_symbols(buffer, nptrs);
    for (int j = 0; j < nptrs; j++)
        printf("%s\n", strings[j]);

    free(strings);
    exit(-1);
}
