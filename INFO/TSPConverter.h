#ifndef TSPCONVERTER_H
#define TSPCONVERTER_H

#include <QObject>
#include <math.h>

class TSPConverter : public QObject
{
    Q_OBJECT
public:
    explicit TSPConverter(QObject *parent = nullptr);

    double getTemperature(double r)
    {
        double t;

        double const A = 3.9083e-3;
        double const B = -5.775e-7;
        double const D1 = 255.819;
        double const D2 = 9.14550;
        double const D3 = -2.92363;
        double const D4 = 1.79090;
        double const Ro = 1000.12;

        if ((r/Ro)>= 1.0)
        {
            t = (sqrt((pow(A, 2) - ((1.0 - (r / Ro)) *
                 0.0004 * B))) - A) / (0.0002 * B);
        }

        else {
            t = D1 * (((r * A) / Ro) - 1.0) +
                D2 * pow(((r * A) / Ro) - 1.0, 2) +
                D3 * pow((((r * A) / Ro) - 1.0), 3) +
                D4 * pow((((r * A) / Ro) - 1.0), 4);
        }

        return t;

    }
signals:

};

#endif // TSPCONVERTER_H
