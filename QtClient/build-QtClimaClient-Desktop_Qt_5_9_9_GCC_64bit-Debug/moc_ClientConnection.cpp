/****************************************************************************
** Meta object code from reading C++ file 'ClientConnection.h'
**
** Created by: The Qt Meta Object Compiler version 67 (Qt 5.9.9)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../QtClimaClient/Network/ClientConnection.h"
#include <QtCore/qbytearray.h>
#include <QtCore/qmetatype.h>
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'ClientConnection.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 67
#error "This file was generated using the moc from 5.9.9. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
QT_WARNING_PUSH
QT_WARNING_DISABLE_DEPRECATED
struct qt_meta_stringdata_ClientConnection_t {
    QByteArrayData data[9];
    char stringdata0[99];
};
#define QT_MOC_LITERAL(idx, ofs, len) \
    Q_STATIC_BYTE_ARRAY_DATA_HEADER_INITIALIZER_WITH_OFFSET(len, \
    qptrdiff(offsetof(qt_meta_stringdata_ClientConnection_t, stringdata0) + ofs \
        - idx * sizeof(QByteArrayData)) \
    )
static const qt_meta_stringdata_ClientConnection_t qt_meta_stringdata_ClientConnection = {
    {
QT_MOC_LITERAL(0, 0, 16), // "ClientConnection"
QT_MOC_LITERAL(1, 17, 13), // "ReplyReceived"
QT_MOC_LITERAL(2, 31, 0), // ""
QT_MOC_LITERAL(3, 32, 12), // "NetworkReply"
QT_MOC_LITERAL(4, 45, 5), // "reply"
QT_MOC_LITERAL(5, 51, 11), // "SendRequest"
QT_MOC_LITERAL(6, 63, 15), // "NetworkRequest*"
QT_MOC_LITERAL(7, 79, 7), // "request"
QT_MOC_LITERAL(8, 87, 11) // "onReadyRead"

    },
    "ClientConnection\0ReplyReceived\0\0"
    "NetworkReply\0reply\0SendRequest\0"
    "NetworkRequest*\0request\0onReadyRead"
};
#undef QT_MOC_LITERAL

static const uint qt_meta_data_ClientConnection[] = {

 // content:
       7,       // revision
       0,       // classname
       0,    0, // classinfo
       3,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       1,       // signalCount

 // signals: name, argc, parameters, tag, flags
       1,    1,   29,    2, 0x06 /* Public */,

 // slots: name, argc, parameters, tag, flags
       5,    1,   32,    2, 0x0a /* Public */,
       8,    0,   35,    2, 0x08 /* Private */,

 // signals: parameters
    QMetaType::Void, 0x80000000 | 3,    4,

 // slots: parameters
    QMetaType::Void, 0x80000000 | 6,    7,
    QMetaType::Void,

       0        // eod
};

void ClientConnection::qt_static_metacall(QObject *_o, QMetaObject::Call _c, int _id, void **_a)
{
    if (_c == QMetaObject::InvokeMetaMethod) {
        ClientConnection *_t = static_cast<ClientConnection *>(_o);
        Q_UNUSED(_t)
        switch (_id) {
        case 0: _t->ReplyReceived((*reinterpret_cast< const NetworkReply(*)>(_a[1]))); break;
        case 1: _t->SendRequest((*reinterpret_cast< NetworkRequest*(*)>(_a[1]))); break;
        case 2: _t->onReadyRead(); break;
        default: ;
        }
    } else if (_c == QMetaObject::IndexOfMethod) {
        int *result = reinterpret_cast<int *>(_a[0]);
        {
            typedef void (ClientConnection::*_t)(const NetworkReply & );
            if (*reinterpret_cast<_t *>(_a[1]) == static_cast<_t>(&ClientConnection::ReplyReceived)) {
                *result = 0;
                return;
            }
        }
    }
}

const QMetaObject ClientConnection::staticMetaObject = {
    { &QObject::staticMetaObject, qt_meta_stringdata_ClientConnection.data,
      qt_meta_data_ClientConnection,  qt_static_metacall, nullptr, nullptr}
};


const QMetaObject *ClientConnection::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->dynamicMetaObject() : &staticMetaObject;
}

void *ClientConnection::qt_metacast(const char *_clname)
{
    if (!_clname) return nullptr;
    if (!strcmp(_clname, qt_meta_stringdata_ClientConnection.stringdata0))
        return static_cast<void*>(this);
    return QObject::qt_metacast(_clname);
}

int ClientConnection::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QObject::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        if (_id < 3)
            qt_static_metacall(this, _c, _id, _a);
        _id -= 3;
    } else if (_c == QMetaObject::RegisterMethodArgumentMetaType) {
        if (_id < 3)
            *reinterpret_cast<int*>(_a[0]) = -1;
        _id -= 3;
    }
    return _id;
}

// SIGNAL 0
void ClientConnection::ReplyReceived(const NetworkReply & _t1)
{
    void *_a[] = { nullptr, const_cast<void*>(reinterpret_cast<const void*>(&_t1)) };
    QMetaObject::activate(this, &staticMetaObject, 0, _a);
}
QT_WARNING_POP
QT_END_MOC_NAMESPACE
