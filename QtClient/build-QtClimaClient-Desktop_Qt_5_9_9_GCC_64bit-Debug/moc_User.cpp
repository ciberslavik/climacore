/****************************************************************************
** Meta object code from reading C++ file 'User.h'
**
** Created by: The Qt Meta Object Compiler version 67 (Qt 5.9.9)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../QtClimaClient/Models/Authorization/User.h"
#include <QtCore/qbytearray.h>
#include <QtCore/qmetatype.h>
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'User.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 67
#error "This file was generated using the moc from 5.9.9. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
QT_WARNING_PUSH
QT_WARNING_DISABLE_DEPRECATED
struct qt_meta_stringdata_User_t {
    QByteArrayData data[6];
    char stringdata0[52];
};
#define QT_MOC_LITERAL(idx, ofs, len) \
    Q_STATIC_BYTE_ARRAY_DATA_HEADER_INITIALIZER_WITH_OFFSET(len, \
    qptrdiff(offsetof(qt_meta_stringdata_User_t, stringdata0) + ofs \
        - idx * sizeof(QByteArrayData)) \
    )
static const qt_meta_stringdata_User_t qt_meta_stringdata_User = {
    {
QT_MOC_LITERAL(0, 0, 4), // "User"
QT_MOC_LITERAL(1, 5, 9), // "FirstName"
QT_MOC_LITERAL(2, 15, 8), // "QDomNode"
QT_MOC_LITERAL(3, 24, 8), // "LastName"
QT_MOC_LITERAL(4, 33, 12), // "PasswordHash"
QT_MOC_LITERAL(5, 46, 5) // "Login"

    },
    "User\0FirstName\0QDomNode\0LastName\0"
    "PasswordHash\0Login"
};
#undef QT_MOC_LITERAL

static const uint qt_meta_data_User[] = {

 // content:
       7,       // revision
       0,       // classname
       0,    0, // classinfo
       0,    0, // methods
       8,   14, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       4,       // flags
       0,       // signalCount

 // properties: name, type, flags
       1, QMetaType::QJsonValue, 0x00095003,
       1, 0x80000000 | 2, 0x0009500b,
       3, QMetaType::QJsonValue, 0x00095003,
       3, 0x80000000 | 2, 0x0009500b,
       4, QMetaType::QJsonValue, 0x00095003,
       4, 0x80000000 | 2, 0x0009500b,
       5, QMetaType::QJsonValue, 0x00095003,
       5, 0x80000000 | 2, 0x0009500b,

       0        // eod
};

void User::qt_static_metacall(QObject *_o, QMetaObject::Call _c, int _id, void **_a)
{
    if (_c == QMetaObject::RegisterPropertyMetaType) {
        switch (_id) {
        default: *reinterpret_cast<int*>(_a[0]) = -1; break;
        case 7:
        case 5:
        case 3:
        case 1:
            *reinterpret_cast<int*>(_a[0]) = qRegisterMetaType< QDomNode >(); break;
        }
    }

#ifndef QT_NO_PROPERTIES
    else if (_c == QMetaObject::ReadProperty) {
        User *_t = reinterpret_cast<User *>(_o);
        Q_UNUSED(_t)
        void *_v = _a[0];
        switch (_id) {
        case 0: *reinterpret_cast< QJsonValue*>(_v) = _t->get_json_FirstName(); break;
        case 1: *reinterpret_cast< QDomNode*>(_v) = _t->get_xml_FirstName(); break;
        case 2: *reinterpret_cast< QJsonValue*>(_v) = _t->get_json_LastName(); break;
        case 3: *reinterpret_cast< QDomNode*>(_v) = _t->get_xml_LastName(); break;
        case 4: *reinterpret_cast< QJsonValue*>(_v) = _t->get_json_PasswordHash(); break;
        case 5: *reinterpret_cast< QDomNode*>(_v) = _t->get_xml_PasswordHash(); break;
        case 6: *reinterpret_cast< QJsonValue*>(_v) = _t->get_json_Login(); break;
        case 7: *reinterpret_cast< QDomNode*>(_v) = _t->get_xml_Login(); break;
        default: break;
        }
    } else if (_c == QMetaObject::WriteProperty) {
        User *_t = reinterpret_cast<User *>(_o);
        Q_UNUSED(_t)
        void *_v = _a[0];
        switch (_id) {
        case 0: _t->set_json_FirstName(*reinterpret_cast< QJsonValue*>(_v)); break;
        case 1: _t->set_xml_FirstName(*reinterpret_cast< QDomNode*>(_v)); break;
        case 2: _t->set_json_LastName(*reinterpret_cast< QJsonValue*>(_v)); break;
        case 3: _t->set_xml_LastName(*reinterpret_cast< QDomNode*>(_v)); break;
        case 4: _t->set_json_PasswordHash(*reinterpret_cast< QJsonValue*>(_v)); break;
        case 5: _t->set_xml_PasswordHash(*reinterpret_cast< QDomNode*>(_v)); break;
        case 6: _t->set_json_Login(*reinterpret_cast< QJsonValue*>(_v)); break;
        case 7: _t->set_xml_Login(*reinterpret_cast< QDomNode*>(_v)); break;
        default: break;
        }
    } else if (_c == QMetaObject::ResetProperty) {
    }
#endif // QT_NO_PROPERTIES
    Q_UNUSED(_o);
}

const QMetaObject User::staticMetaObject = {
    { &QSerializer::staticMetaObject, qt_meta_stringdata_User.data,
      qt_meta_data_User,  qt_static_metacall, nullptr, nullptr}
};

QT_WARNING_POP
QT_END_MOC_NAMESPACE
