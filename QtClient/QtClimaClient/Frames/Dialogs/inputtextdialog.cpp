#include "inputtextdialog.h"
#include "ui_inputtextdialog.h"

#include <QGridLayout>
#include <QLineEdit>
#include <QPushButton>
#include <QSignalMapper>
#include <QSizePolicy>

#define NEXT_ROW_MARKER 0
#define WHITESPACE_MARKER -1
struct KeyboardLayoutEntry{
    int key;
    const QString label;
};

KeyboardLayoutEntry keyboardLayout[] = {
    { Qt::Key_1, "1" },
    { Qt::Key_2, "2" },
    { Qt::Key_3, "3" },
    { Qt::Key_4, "4" },
    { Qt::Key_5, "5" },
    { Qt::Key_6, "6" },
    { Qt::Key_7, "7" },
    { Qt::Key_8, "8" },
    { Qt::Key_9, "9" },
    { Qt::Key_0, "0" },
    { Qt::Key_Backspace, "<-" },
    { NEXT_ROW_MARKER, 0 },
    { Qt::Key_Q, "Й" },
    { Qt::Key_W, "Ц" },
    { Qt::Key_E, "У" },
    { Qt::Key_R, "К" },
    { Qt::Key_T, "Е" },
    { Qt::Key_Z, "Н" },
    { Qt::Key_U, "Г" },
    { Qt::Key_I, "Ш" },
    { Qt::Key_O, "Щ" },
    { Qt::Key_P, "З" },
    { Qt::Key_BracketLeft, "Х" },
    { Qt::Key_BracketRight, "Ъ" },
    { NEXT_ROW_MARKER, 0 },
    { Qt::Key_A, "Ф" },
    { Qt::Key_S, "Ы" },
    { Qt::Key_D, "В" },
    { Qt::Key_F, "А" },
    { Qt::Key_G, "П" },
    { Qt::Key_H, "Р" },
    { Qt::Key_J, "О" },
    { Qt::Key_K, "Л" },
    { Qt::Key_L, "Д" },
    { Qt::Key_Semicolon, "Ж" },
    { Qt::Key_QuoteDbl, "Э" },
    { NEXT_ROW_MARKER, 0 },
    { Qt::Key_Y, "Я" },
    { Qt::Key_X, "Ч" },
    { Qt::Key_C, "С" },
    { Qt::Key_V, "М" },
    { Qt::Key_B, "И" },
    { Qt::Key_N, "Т" },
    { Qt::Key_M, "Ь" },
    { Qt::Key_Comma, "Б" },
    { Qt::Key_Period, "Ю" },
    { Qt::Key_Slash, "." },
    { Qt::Key_Enter, "Enter" },
    { NEXT_ROW_MARKER, 0 },
    { Qt::Key_Space, " " }
};

const static int layoutSize = (sizeof(keyboardLayout) / sizeof(KeyboardLayoutEntry));

static QString keyToCharacter(int key)
{
    for (int i = 0; i < layoutSize; ++i) {
        if (keyboardLayout[i].key == key)
            return keyboardLayout[i].label;
    }

    return QString();
}


InputTextDialog::InputTextDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::InputTextDialog)
{
    ui->setupUi(this);

    QGridLayout *grid = new QGridLayout(this);
    setLayout(grid);
    QSignalMapper *mapper = new QSignalMapper(this);
    connect(mapper, SIGNAL(mapped(int)), SLOT(buttonClicked(int)));

    QFont fnt = font();
    fnt.setPointSizeF(14);

    lblTitle = new QLabel(this);
    lblTitle->setText("hdjfhd");
    lblTitle->setFont(fnt);
    lblTitle->setAlignment(Qt::AlignHCenter | Qt::AlignVCenter);
    textbox = new QLineEdit(this);
    textbox->setFont(fnt);
    grid->addWidget(lblTitle,0,1,1,11);
    grid->addWidget(textbox,1,1,1,11);
    int row = 2;
    int column = 1;

    grid->addItem(new QSpacerItem(0,10, QSizePolicy::Expanding, QSizePolicy::Expanding),0,0);
    for (int i = 0; i < layoutSize; ++i) {
        if (keyboardLayout[i].key == NEXT_ROW_MARKER) {
            row++;
            column = 0;

            QSpacerItem *spacer = new QSpacerItem(0,10);


            grid->addItem(spacer, row, column++);
            continue;
        }

        QPushButton *button = new QPushButton(this);
        //button->setLayout(grid);
        button->setFixedWidth(50);
        button->setFixedHeight(50);
        button->setText(keyboardLayout[i].label);
        QFont font = button->font();
        font.setWeight(16);
        button->setFont(font);
        mapper->setMapping(button, keyboardLayout[i].key);
        connect(button, SIGNAL(clicked()), mapper, SLOT(map()));

        if(keyboardLayout[i].key==Qt::Key_Backspace)
        {
            QIcon *icon = new QIcon(":/Images/backspace-arrow.png");
            button->setIcon(*icon);
            button->setIconSize(QSize(24,24));
            button->setText("");
            button->setFixedWidth(100);
            grid->addWidget(button, row, column,1,2,Qt::AlignHCenter);
        }
        else if(keyboardLayout[i].key==Qt::Key_Enter)
        {
            button->setFixedWidth(100);
            grid->addWidget(button, row, column,1,2,Qt::AlignHCenter);
        }
        else if(keyboardLayout[i].key==Qt::Key_Space)
        {
            button->setFixedWidth(250);
           //button->setLayout(grid);
            grid->addWidget(button, row, column+2,1,5,Qt::AlignHCenter);
        }
        else
        {
            grid->addWidget(button, row, column);
        }
        column++;
    }
}

InputTextDialog::~InputTextDialog()
{
    delete ui;
}

QString InputTextDialog::getText()
{
    return textbox->text();
}

void InputTextDialog::setText(QString text)
{
    textbox->setText(text);
}

void InputTextDialog::setTitle(const QString &title)
{
    lblTitle->setText(title);
}

void InputTextDialog::buttonClicked(int key)
{
    if(key==Qt::Key_Enter)
    {
        accept();
        return;
    }
    else if(key==Qt::Key_Backspace)
    {
        QString txt = textbox->text();
        txt.chop(1);
        textbox->setText(txt);
    }
    else
    {
        QString character = keyToCharacter(key);
        textbox->setText(textbox->text()+character);
    }
}

void InputTextDialog::createKeyboard()
{



}
