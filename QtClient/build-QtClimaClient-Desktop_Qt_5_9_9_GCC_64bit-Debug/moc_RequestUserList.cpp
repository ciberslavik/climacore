/****************************************************************************
** Meta object code from reading C++ file 'RequestUserList.h'
**
** Created by: The Qt Meta Object Compiler version 67 (Qt 5.9.9)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../QtClimaClient/Network/RequestUserList.h"
#include <QtCore/qbytearray.h>
#include <QtCore/qmetatype.h>
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'RequestUserList.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 67
#error "This file was generated using the moc from 5.9.9. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
QT_WARNING_PUSH
QT_WARNING_DISABLE_DEPRECATED
struct qt_meta_stringdata_RequestUserList_t {
    QByteArrayData data[4];
    char stringdata0[48];
};
#define QT_MOC_LITERAL(idx, ofs, len) \
    Q_STATIC_BYTE_ARRAY_DATA_HEADER_INITIALIZER_WITH_OFFSET(len, \
    qptrdiff(offsetof(qt_meta_stringdata_RequestUserList_t, stringdata0) + ofs \
        - idx * sizeof(QByteArrayData)) \
    )
static const qt_meta_stringdata_RequestUserList_t qt_meta_stringdata_RequestUserList = {
    {
QT_MOC_LITERAL(0, 0, 15), // "RequestUserList"
QT_MOC_LITERAL(1, 16, 11), // "RequestType"
QT_MOC_LITERAL(2, 28, 8), // "QDomNode"
QT_MOC_LITERAL(3, 37, 10) // "Parameters"

    },
    "RequestUserList\0RequestType\0QDomNode\0"
    "Parameters"
};
#undef QT_MOC_LITERAL

static const uint qt_meta_data_RequestUserList[] = {

 // content:
       7,       // revision
       0,       // classname
       0,    0, // classinfo
       0,    0, // methods
       4,   14, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       4,       // flags
       0,       // signalCount

 // properties: name, type, flags
       1, QMetaType::QJsonValue, 0x00095003,
       1, 0x80000000 | 2, 0x0009500b,
       3, QMetaType::QJsonValue, 0x00095003,
       3, 0x80000000 | 2, 0x0009500b,

       0        // eod
};

void RequestUserList::qt_static_metacall(QObject *_o, QMetaObject::Call _c, int _id, void **_a)
{
    if (_c == QMetaObject::RegisterPropertyMetaType) {
        switch (_id) {
        default: *reinterpret_cast<int*>(_a[0]) = -1; break;
        case 3:
        case 1:
            *reinterpret_cast<int*>(_a[0]) = qRegisterMetaType< QDomNode >(); break;
        }
    }

#ifndef QT_NO_PROPERTIES
    else if (_c == QMetaObject::ReadProperty) {
        RequestUserList *_t = reinterpret_cast<RequestUserList *>(_o);
        Q_UNUSED(_t)
        void *_v = _a[0];
        switch (_id) {
        case 0: *reinterpret_cast< QJsonValue*>(_v) = _t->get_json_RequestType(); break;
        case 1: *reinterpret_cast< QDomNode*>(_v) = _t->get_xml_RequestType(); break;
        case 2: *reinterpret_cast< QJsonValue*>(_v) = _t->get_json_Parameters(); break;
        case 3: *reinterpret_cast< QDomNode*>(_v) = _t->get_xml_Parameters(); break;
        default: break;
        }
    } else if (_c == QMetaObject::WriteProperty) {
        RequestUserList *_t = reinterpret_cast<RequestUserList *>(_o);
        Q_UNUSED(_t)
        void *_v = _a[0];
        switch (_id) {
        case 0: _t->set_json_RequestType(*reinterpret_cast< QJsonValue*>(_v)); break;
        case 1: _t->set_xml_RequestType(*reinterpret_cast< QDomNode*>(_v)); break;
        case 2: _t->set_json_Parameters(*reinterpret_cast< QJsonValue*>(_v)); break;
        case 3: _t->set_xml_Parameters(*reinterpret_cast< QDomNode*>(_v)); break;
        default: break;
        }
    } else if (_c == QMetaObject::ResetProperty) {
    }
#endif // QT_NO_PROPERTIES
    Q_UNUSED(_o);
}

const QMetaObject RequestUserList::staticMetaObject = {
    { &QSerializer::staticMetaObject, qt_meta_stringdata_RequestUserList.data,
      qt_meta_data_RequestUserList,  qt_static_metacall, nullptr, nullptr}
};

QT_WARNING_POP
QT_END_MOC_NAMESPACE