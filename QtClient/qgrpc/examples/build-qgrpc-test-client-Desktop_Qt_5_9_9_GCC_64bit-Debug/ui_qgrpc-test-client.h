/********************************************************************************
** Form generated from reading UI file 'qgrpc-test-client.ui'
**
** Created by: Qt User Interface Compiler version 5.9.9
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_QGRPC_2D_TEST_2D_CLIENT_H
#define UI_QGRPC_2D_TEST_2D_CLIENT_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QFrame>
#include <QtWidgets/QGridLayout>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QLabel>
#include <QtWidgets/QLineEdit>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QSpacerItem>
#include <QtWidgets/QSpinBox>
#include <QtWidgets/QTextEdit>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_mainWindow
{
public:
    QWidget *centralWidget;
    QGridLayout *gridLayout_7;
    QGridLayout *gridLayout;
    QPushButton *pb_sayhello;
    QPushButton *pb_start_read_write_stream;
    QPushButton *pb_stop_read_write_stream;
    QPushButton *pb_stop_write_stream;
    QPushButton *pb_start_write_stream;
    QPushButton *pb_start_read_stream;
    QFrame *line;
    QGridLayout *gridLayout_5;
    QGridLayout *gridLayout_2;
    QLineEdit *le_ipaddr;
    QLabel *label_2;
    QGridLayout *gridLayout_3;
    QSpinBox *sb_port;
    QLabel *label_3;
    QPushButton *pb_connect;
    QPushButton *pb_disconnect;
    QPushButton *pb_reconnect;
    QGridLayout *gridLayout_6;
    QSpacerItem *verticalSpacer;
    QGridLayout *gridLayout_4;
    QLabel *label;
    QPushButton *pb_indicator;
    QSpacerItem *horizontalSpacer;
    QTextEdit *textEdit;
    QMenuBar *menubar;

    void setupUi(QMainWindow *mainWindow)
    {
        if (mainWindow->objectName().isEmpty())
            mainWindow->setObjectName(QStringLiteral("mainWindow"));
        mainWindow->resize(908, 735);
        centralWidget = new QWidget(mainWindow);
        centralWidget->setObjectName(QStringLiteral("centralWidget"));
        gridLayout_7 = new QGridLayout(centralWidget);
        gridLayout_7->setObjectName(QStringLiteral("gridLayout_7"));
        gridLayout = new QGridLayout();
        gridLayout->setObjectName(QStringLiteral("gridLayout"));
        pb_sayhello = new QPushButton(centralWidget);
        pb_sayhello->setObjectName(QStringLiteral("pb_sayhello"));

        gridLayout->addWidget(pb_sayhello, 0, 0, 1, 1);

        pb_start_read_write_stream = new QPushButton(centralWidget);
        pb_start_read_write_stream->setObjectName(QStringLiteral("pb_start_read_write_stream"));

        gridLayout->addWidget(pb_start_read_write_stream, 0, 3, 1, 1);

        pb_stop_read_write_stream = new QPushButton(centralWidget);
        pb_stop_read_write_stream->setObjectName(QStringLiteral("pb_stop_read_write_stream"));

        gridLayout->addWidget(pb_stop_read_write_stream, 1, 3, 1, 1);

        pb_stop_write_stream = new QPushButton(centralWidget);
        pb_stop_write_stream->setObjectName(QStringLiteral("pb_stop_write_stream"));

        gridLayout->addWidget(pb_stop_write_stream, 1, 2, 1, 1);

        pb_start_write_stream = new QPushButton(centralWidget);
        pb_start_write_stream->setObjectName(QStringLiteral("pb_start_write_stream"));

        gridLayout->addWidget(pb_start_write_stream, 0, 2, 1, 1);

        pb_start_read_stream = new QPushButton(centralWidget);
        pb_start_read_stream->setObjectName(QStringLiteral("pb_start_read_stream"));

        gridLayout->addWidget(pb_start_read_stream, 0, 1, 1, 1);


        gridLayout_7->addLayout(gridLayout, 0, 0, 1, 2);

        line = new QFrame(centralWidget);
        line->setObjectName(QStringLiteral("line"));
        line->setFrameShape(QFrame::HLine);
        line->setFrameShadow(QFrame::Sunken);

        gridLayout_7->addWidget(line, 1, 0, 1, 2);

        gridLayout_5 = new QGridLayout();
        gridLayout_5->setObjectName(QStringLiteral("gridLayout_5"));
        gridLayout_2 = new QGridLayout();
        gridLayout_2->setObjectName(QStringLiteral("gridLayout_2"));
        le_ipaddr = new QLineEdit(centralWidget);
        le_ipaddr->setObjectName(QStringLiteral("le_ipaddr"));

        gridLayout_2->addWidget(le_ipaddr, 0, 0, 1, 1);

        label_2 = new QLabel(centralWidget);
        label_2->setObjectName(QStringLiteral("label_2"));

        gridLayout_2->addWidget(label_2, 0, 1, 1, 1);


        gridLayout_5->addLayout(gridLayout_2, 0, 0, 1, 2);

        gridLayout_3 = new QGridLayout();
        gridLayout_3->setObjectName(QStringLiteral("gridLayout_3"));
        sb_port = new QSpinBox(centralWidget);
        sb_port->setObjectName(QStringLiteral("sb_port"));
        sb_port->setMinimum(1);
        sb_port->setMaximum(65535);
        sb_port->setValue(50051);

        gridLayout_3->addWidget(sb_port, 0, 0, 1, 1);

        label_3 = new QLabel(centralWidget);
        label_3->setObjectName(QStringLiteral("label_3"));

        gridLayout_3->addWidget(label_3, 0, 1, 1, 1);


        gridLayout_5->addLayout(gridLayout_3, 0, 2, 1, 1);

        pb_connect = new QPushButton(centralWidget);
        pb_connect->setObjectName(QStringLiteral("pb_connect"));

        gridLayout_5->addWidget(pb_connect, 1, 0, 1, 1);

        pb_disconnect = new QPushButton(centralWidget);
        pb_disconnect->setObjectName(QStringLiteral("pb_disconnect"));

        gridLayout_5->addWidget(pb_disconnect, 1, 1, 1, 1);

        pb_reconnect = new QPushButton(centralWidget);
        pb_reconnect->setObjectName(QStringLiteral("pb_reconnect"));

        gridLayout_5->addWidget(pb_reconnect, 1, 2, 1, 1);


        gridLayout_7->addLayout(gridLayout_5, 2, 0, 2, 1);

        gridLayout_6 = new QGridLayout();
        gridLayout_6->setObjectName(QStringLiteral("gridLayout_6"));
        verticalSpacer = new QSpacerItem(20, 20, QSizePolicy::Minimum, QSizePolicy::Minimum);

        gridLayout_6->addItem(verticalSpacer, 0, 0, 1, 1);

        gridLayout_4 = new QGridLayout();
        gridLayout_4->setObjectName(QStringLiteral("gridLayout_4"));
        label = new QLabel(centralWidget);
        label->setObjectName(QStringLiteral("label"));

        gridLayout_4->addWidget(label, 0, 2, 1, 1);

        pb_indicator = new QPushButton(centralWidget);
        pb_indicator->setObjectName(QStringLiteral("pb_indicator"));
        pb_indicator->setEnabled(true);
        pb_indicator->setMaximumSize(QSize(20, 20));
        pb_indicator->setFlat(true);

        gridLayout_4->addWidget(pb_indicator, 0, 1, 1, 1);

        horizontalSpacer = new QSpacerItem(40, 20, QSizePolicy::Expanding, QSizePolicy::Minimum);

        gridLayout_4->addItem(horizontalSpacer, 0, 0, 1, 1);


        gridLayout_6->addLayout(gridLayout_4, 1, 0, 1, 1);


        gridLayout_7->addLayout(gridLayout_6, 3, 1, 1, 1);

        textEdit = new QTextEdit(centralWidget);
        textEdit->setObjectName(QStringLiteral("textEdit"));
        textEdit->setReadOnly(true);

        gridLayout_7->addWidget(textEdit, 4, 0, 1, 2);

        mainWindow->setCentralWidget(centralWidget);
        line->raise();
        textEdit->raise();
        pb_indicator->raise();
        pb_indicator->raise();
        le_ipaddr->raise();
        sb_port->raise();
        label_2->raise();
        label_3->raise();
        menubar = new QMenuBar(mainWindow);
        menubar->setObjectName(QStringLiteral("menubar"));
        menubar->setGeometry(QRect(0, 0, 908, 21));
        mainWindow->setMenuBar(menubar);

        retranslateUi(mainWindow);

        QMetaObject::connectSlotsByName(mainWindow);
    } // setupUi

    void retranslateUi(QMainWindow *mainWindow)
    {
        mainWindow->setWindowTitle(QApplication::translate("mainWindow", "MainWindow", Q_NULLPTR));
        pb_sayhello->setText(QApplication::translate("mainWindow", "Say hello", Q_NULLPTR));
        pb_start_read_write_stream->setText(QApplication::translate("mainWindow", "start \"Both glad to see\"", Q_NULLPTR));
        pb_stop_read_write_stream->setText(QApplication::translate("mainWindow", "stop \"Both glad to see\"", Q_NULLPTR));
        pb_stop_write_stream->setText(QApplication::translate("mainWindow", "stop \"Glad to see you\"", Q_NULLPTR));
        pb_start_write_stream->setText(QApplication::translate("mainWindow", "start \"Glad to see you\"", Q_NULLPTR));
        pb_start_read_stream->setText(QApplication::translate("mainWindow", "start \"Glad to see me\"", Q_NULLPTR));
        le_ipaddr->setText(QApplication::translate("mainWindow", "172.23.148.117", Q_NULLPTR));
        label_2->setText(QApplication::translate("mainWindow", "ip", Q_NULLPTR));
        label_3->setText(QApplication::translate("mainWindow", "port", Q_NULLPTR));
        pb_connect->setText(QApplication::translate("mainWindow", "connect", Q_NULLPTR));
        pb_disconnect->setText(QApplication::translate("mainWindow", "disconnect", Q_NULLPTR));
        pb_reconnect->setText(QApplication::translate("mainWindow", "reconnect", Q_NULLPTR));
        label->setText(QApplication::translate("mainWindow", "connection indicator", Q_NULLPTR));
        pb_indicator->setText(QString());
    } // retranslateUi

};

namespace Ui {
    class mainWindow: public Ui_mainWindow {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_QGRPC_2D_TEST_2D_CLIENT_H
