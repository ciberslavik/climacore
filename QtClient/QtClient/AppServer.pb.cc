// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: AppServer.proto

#include "AppServer.pb.h"

#include <algorithm>

#include <google/protobuf/io/coded_stream.h>
#include <google/protobuf/extension_set.h>
#include <google/protobuf/wire_format_lite.h>
#include <google/protobuf/descriptor.h>
#include <google/protobuf/generated_message_reflection.h>
#include <google/protobuf/reflection_ops.h>
#include <google/protobuf/wire_format.h>
// @@protoc_insertion_point(includes)
#include <google/protobuf/port_def.inc>

PROTOBUF_PRAGMA_INIT_SEG
namespace clima_server {
constexpr ServerInfoRequest::ServerInfoRequest(
  ::PROTOBUF_NAMESPACE_ID::internal::ConstantInitialized){}
struct ServerInfoRequestDefaultTypeInternal {
  constexpr ServerInfoRequestDefaultTypeInternal()
    : _instance(::PROTOBUF_NAMESPACE_ID::internal::ConstantInitialized{}) {}
  ~ServerInfoRequestDefaultTypeInternal() {}
  union {
    ServerInfoRequest _instance;
  };
};
PROTOBUF_ATTRIBUTE_NO_DESTROY PROTOBUF_CONSTINIT ServerInfoRequestDefaultTypeInternal _ServerInfoRequest_default_instance_;
constexpr ServerInfoReply::ServerInfoReply(
  ::PROTOBUF_NAMESPACE_ID::internal::ConstantInitialized)
  : servername_(&::PROTOBUF_NAMESPACE_ID::internal::fixed_address_empty_string)
  , versionminor_(0)
  , versionmajor_(0){}
struct ServerInfoReplyDefaultTypeInternal {
  constexpr ServerInfoReplyDefaultTypeInternal()
    : _instance(::PROTOBUF_NAMESPACE_ID::internal::ConstantInitialized{}) {}
  ~ServerInfoReplyDefaultTypeInternal() {}
  union {
    ServerInfoReply _instance;
  };
};
PROTOBUF_ATTRIBUTE_NO_DESTROY PROTOBUF_CONSTINIT ServerInfoReplyDefaultTypeInternal _ServerInfoReply_default_instance_;
}  // namespace clima_server
static ::PROTOBUF_NAMESPACE_ID::Metadata file_level_metadata_AppServer_2eproto[2];
static constexpr ::PROTOBUF_NAMESPACE_ID::EnumDescriptor const** file_level_enum_descriptors_AppServer_2eproto = nullptr;
static constexpr ::PROTOBUF_NAMESPACE_ID::ServiceDescriptor const** file_level_service_descriptors_AppServer_2eproto = nullptr;

const ::PROTOBUF_NAMESPACE_ID::uint32 TableStruct_AppServer_2eproto::offsets[] PROTOBUF_SECTION_VARIABLE(protodesc_cold) = {
  ~0u,  // no _has_bits_
  PROTOBUF_FIELD_OFFSET(::clima_server::ServerInfoRequest, _internal_metadata_),
  ~0u,  // no _extensions_
  ~0u,  // no _oneof_case_
  ~0u,  // no _weak_field_map_
  ~0u,  // no _has_bits_
  PROTOBUF_FIELD_OFFSET(::clima_server::ServerInfoReply, _internal_metadata_),
  ~0u,  // no _extensions_
  ~0u,  // no _oneof_case_
  ~0u,  // no _weak_field_map_
  PROTOBUF_FIELD_OFFSET(::clima_server::ServerInfoReply, servername_),
  PROTOBUF_FIELD_OFFSET(::clima_server::ServerInfoReply, versionminor_),
  PROTOBUF_FIELD_OFFSET(::clima_server::ServerInfoReply, versionmajor_),
};
static const ::PROTOBUF_NAMESPACE_ID::internal::MigrationSchema schemas[] PROTOBUF_SECTION_VARIABLE(protodesc_cold) = {
  { 0, -1, sizeof(::clima_server::ServerInfoRequest)},
  { 5, -1, sizeof(::clima_server::ServerInfoReply)},
};

static ::PROTOBUF_NAMESPACE_ID::Message const * const file_default_instances[] = {
  reinterpret_cast<const ::PROTOBUF_NAMESPACE_ID::Message*>(&::clima_server::_ServerInfoRequest_default_instance_),
  reinterpret_cast<const ::PROTOBUF_NAMESPACE_ID::Message*>(&::clima_server::_ServerInfoReply_default_instance_),
};

const char descriptor_table_protodef_AppServer_2eproto[] PROTOBUF_SECTION_VARIABLE(protodesc_cold) =
  "\n\017AppServer.proto\022\014clima_server\"\023\n\021Serve"
  "rInfoRequest\"Q\n\017ServerInfoReply\022\022\n\nServe"
  "rName\030\001 \001(\t\022\024\n\014VersionMinor\030\002 \001(\005\022\024\n\014Ver"
  "sionMajor\030\003 \001(\0052Y\n\tAppServer\022L\n\nServerIn"
  "fo\022\037.clima_server.ServerInfoRequest\032\035.cl"
  "ima_server.ServerInfoReplyb\006proto3"
  ;
static ::PROTOBUF_NAMESPACE_ID::internal::once_flag descriptor_table_AppServer_2eproto_once;
const ::PROTOBUF_NAMESPACE_ID::internal::DescriptorTable descriptor_table_AppServer_2eproto = {
  false, false, 234, descriptor_table_protodef_AppServer_2eproto, "AppServer.proto", 
  &descriptor_table_AppServer_2eproto_once, nullptr, 0, 2,
  schemas, file_default_instances, TableStruct_AppServer_2eproto::offsets,
  file_level_metadata_AppServer_2eproto, file_level_enum_descriptors_AppServer_2eproto, file_level_service_descriptors_AppServer_2eproto,
};
PROTOBUF_ATTRIBUTE_WEAK ::PROTOBUF_NAMESPACE_ID::Metadata
descriptor_table_AppServer_2eproto_metadata_getter(int index) {
  ::PROTOBUF_NAMESPACE_ID::internal::AssignDescriptors(&descriptor_table_AppServer_2eproto);
  return descriptor_table_AppServer_2eproto.file_level_metadata[index];
}

// Force running AddDescriptors() at dynamic initialization time.
PROTOBUF_ATTRIBUTE_INIT_PRIORITY static ::PROTOBUF_NAMESPACE_ID::internal::AddDescriptorsRunner dynamic_init_dummy_AppServer_2eproto(&descriptor_table_AppServer_2eproto);
namespace clima_server {

// ===================================================================

class ServerInfoRequest::_Internal {
 public:
};

ServerInfoRequest::ServerInfoRequest(::PROTOBUF_NAMESPACE_ID::Arena* arena)
  : ::PROTOBUF_NAMESPACE_ID::Message(arena) {
  SharedCtor();
  RegisterArenaDtor(arena);
  // @@protoc_insertion_point(arena_constructor:clima_server.ServerInfoRequest)
}
ServerInfoRequest::ServerInfoRequest(const ServerInfoRequest& from)
  : ::PROTOBUF_NAMESPACE_ID::Message() {
  _internal_metadata_.MergeFrom<::PROTOBUF_NAMESPACE_ID::UnknownFieldSet>(from._internal_metadata_);
  // @@protoc_insertion_point(copy_constructor:clima_server.ServerInfoRequest)
}

void ServerInfoRequest::SharedCtor() {
}

ServerInfoRequest::~ServerInfoRequest() {
  // @@protoc_insertion_point(destructor:clima_server.ServerInfoRequest)
  SharedDtor();
  _internal_metadata_.Delete<::PROTOBUF_NAMESPACE_ID::UnknownFieldSet>();
}

void ServerInfoRequest::SharedDtor() {
  GOOGLE_DCHECK(GetArena() == nullptr);
}

void ServerInfoRequest::ArenaDtor(void* object) {
  ServerInfoRequest* _this = reinterpret_cast< ServerInfoRequest* >(object);
  (void)_this;
}
void ServerInfoRequest::RegisterArenaDtor(::PROTOBUF_NAMESPACE_ID::Arena*) {
}
void ServerInfoRequest::SetCachedSize(int size) const {
  _cached_size_.Set(size);
}

void ServerInfoRequest::Clear() {
// @@protoc_insertion_point(message_clear_start:clima_server.ServerInfoRequest)
  ::PROTOBUF_NAMESPACE_ID::uint32 cached_has_bits = 0;
  // Prevent compiler warnings about cached_has_bits being unused
  (void) cached_has_bits;

  _internal_metadata_.Clear<::PROTOBUF_NAMESPACE_ID::UnknownFieldSet>();
}

const char* ServerInfoRequest::_InternalParse(const char* ptr, ::PROTOBUF_NAMESPACE_ID::internal::ParseContext* ctx) {
#define CHK_(x) if (PROTOBUF_PREDICT_FALSE(!(x))) goto failure
  while (!ctx->Done(&ptr)) {
    ::PROTOBUF_NAMESPACE_ID::uint32 tag;
    ptr = ::PROTOBUF_NAMESPACE_ID::internal::ReadTag(ptr, &tag);
    CHK_(ptr);
        if ((tag & 7) == 4 || tag == 0) {
          ctx->SetLastTag(tag);
          goto success;
        }
        ptr = UnknownFieldParse(tag,
            _internal_metadata_.mutable_unknown_fields<::PROTOBUF_NAMESPACE_ID::UnknownFieldSet>(),
            ptr, ctx);
        CHK_(ptr != nullptr);
        continue;
  }  // while
success:
  return ptr;
failure:
  ptr = nullptr;
  goto success;
#undef CHK_
}

::PROTOBUF_NAMESPACE_ID::uint8* ServerInfoRequest::_InternalSerialize(
    ::PROTOBUF_NAMESPACE_ID::uint8* target, ::PROTOBUF_NAMESPACE_ID::io::EpsCopyOutputStream* stream) const {
  // @@protoc_insertion_point(serialize_to_array_start:clima_server.ServerInfoRequest)
  ::PROTOBUF_NAMESPACE_ID::uint32 cached_has_bits = 0;
  (void) cached_has_bits;

  if (PROTOBUF_PREDICT_FALSE(_internal_metadata_.have_unknown_fields())) {
    target = ::PROTOBUF_NAMESPACE_ID::internal::WireFormat::InternalSerializeUnknownFieldsToArray(
        _internal_metadata_.unknown_fields<::PROTOBUF_NAMESPACE_ID::UnknownFieldSet>(::PROTOBUF_NAMESPACE_ID::UnknownFieldSet::default_instance), target, stream);
  }
  // @@protoc_insertion_point(serialize_to_array_end:clima_server.ServerInfoRequest)
  return target;
}

size_t ServerInfoRequest::ByteSizeLong() const {
// @@protoc_insertion_point(message_byte_size_start:clima_server.ServerInfoRequest)
  size_t total_size = 0;

  ::PROTOBUF_NAMESPACE_ID::uint32 cached_has_bits = 0;
  // Prevent compiler warnings about cached_has_bits being unused
  (void) cached_has_bits;

  if (PROTOBUF_PREDICT_FALSE(_internal_metadata_.have_unknown_fields())) {
    return ::PROTOBUF_NAMESPACE_ID::internal::ComputeUnknownFieldsSize(
        _internal_metadata_, total_size, &_cached_size_);
  }
  int cached_size = ::PROTOBUF_NAMESPACE_ID::internal::ToCachedSize(total_size);
  SetCachedSize(cached_size);
  return total_size;
}

void ServerInfoRequest::MergeFrom(const ::PROTOBUF_NAMESPACE_ID::Message& from) {
// @@protoc_insertion_point(generalized_merge_from_start:clima_server.ServerInfoRequest)
  GOOGLE_DCHECK_NE(&from, this);
  const ServerInfoRequest* source =
      ::PROTOBUF_NAMESPACE_ID::DynamicCastToGenerated<ServerInfoRequest>(
          &from);
  if (source == nullptr) {
  // @@protoc_insertion_point(generalized_merge_from_cast_fail:clima_server.ServerInfoRequest)
    ::PROTOBUF_NAMESPACE_ID::internal::ReflectionOps::Merge(from, this);
  } else {
  // @@protoc_insertion_point(generalized_merge_from_cast_success:clima_server.ServerInfoRequest)
    MergeFrom(*source);
  }
}

void ServerInfoRequest::MergeFrom(const ServerInfoRequest& from) {
// @@protoc_insertion_point(class_specific_merge_from_start:clima_server.ServerInfoRequest)
  GOOGLE_DCHECK_NE(&from, this);
  _internal_metadata_.MergeFrom<::PROTOBUF_NAMESPACE_ID::UnknownFieldSet>(from._internal_metadata_);
  ::PROTOBUF_NAMESPACE_ID::uint32 cached_has_bits = 0;
  (void) cached_has_bits;

}

void ServerInfoRequest::CopyFrom(const ::PROTOBUF_NAMESPACE_ID::Message& from) {
// @@protoc_insertion_point(generalized_copy_from_start:clima_server.ServerInfoRequest)
  if (&from == this) return;
  Clear();
  MergeFrom(from);
}

void ServerInfoRequest::CopyFrom(const ServerInfoRequest& from) {
// @@protoc_insertion_point(class_specific_copy_from_start:clima_server.ServerInfoRequest)
  if (&from == this) return;
  Clear();
  MergeFrom(from);
}

bool ServerInfoRequest::IsInitialized() const {
  return true;
}

void ServerInfoRequest::InternalSwap(ServerInfoRequest* other) {
  using std::swap;
  _internal_metadata_.Swap<::PROTOBUF_NAMESPACE_ID::UnknownFieldSet>(&other->_internal_metadata_);
}

::PROTOBUF_NAMESPACE_ID::Metadata ServerInfoRequest::GetMetadata() const {
  return GetMetadataStatic();
}


// ===================================================================

class ServerInfoReply::_Internal {
 public:
};

ServerInfoReply::ServerInfoReply(::PROTOBUF_NAMESPACE_ID::Arena* arena)
  : ::PROTOBUF_NAMESPACE_ID::Message(arena) {
  SharedCtor();
  RegisterArenaDtor(arena);
  // @@protoc_insertion_point(arena_constructor:clima_server.ServerInfoReply)
}
ServerInfoReply::ServerInfoReply(const ServerInfoReply& from)
  : ::PROTOBUF_NAMESPACE_ID::Message() {
  _internal_metadata_.MergeFrom<::PROTOBUF_NAMESPACE_ID::UnknownFieldSet>(from._internal_metadata_);
  servername_.UnsafeSetDefault(&::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited());
  if (!from._internal_servername().empty()) {
    servername_.Set(::PROTOBUF_NAMESPACE_ID::internal::ArenaStringPtr::EmptyDefault{}, from._internal_servername(), 
      GetArena());
  }
  ::memcpy(&versionminor_, &from.versionminor_,
    static_cast<size_t>(reinterpret_cast<char*>(&versionmajor_) -
    reinterpret_cast<char*>(&versionminor_)) + sizeof(versionmajor_));
  // @@protoc_insertion_point(copy_constructor:clima_server.ServerInfoReply)
}

void ServerInfoReply::SharedCtor() {
servername_.UnsafeSetDefault(&::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited());
::memset(reinterpret_cast<char*>(this) + static_cast<size_t>(
    reinterpret_cast<char*>(&versionminor_) - reinterpret_cast<char*>(this)),
    0, static_cast<size_t>(reinterpret_cast<char*>(&versionmajor_) -
    reinterpret_cast<char*>(&versionminor_)) + sizeof(versionmajor_));
}

ServerInfoReply::~ServerInfoReply() {
  // @@protoc_insertion_point(destructor:clima_server.ServerInfoReply)
  SharedDtor();
  _internal_metadata_.Delete<::PROTOBUF_NAMESPACE_ID::UnknownFieldSet>();
}

void ServerInfoReply::SharedDtor() {
  GOOGLE_DCHECK(GetArena() == nullptr);
  servername_.DestroyNoArena(&::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited());
}

void ServerInfoReply::ArenaDtor(void* object) {
  ServerInfoReply* _this = reinterpret_cast< ServerInfoReply* >(object);
  (void)_this;
}
void ServerInfoReply::RegisterArenaDtor(::PROTOBUF_NAMESPACE_ID::Arena*) {
}
void ServerInfoReply::SetCachedSize(int size) const {
  _cached_size_.Set(size);
}

void ServerInfoReply::Clear() {
// @@protoc_insertion_point(message_clear_start:clima_server.ServerInfoReply)
  ::PROTOBUF_NAMESPACE_ID::uint32 cached_has_bits = 0;
  // Prevent compiler warnings about cached_has_bits being unused
  (void) cached_has_bits;

  servername_.ClearToEmpty();
  ::memset(&versionminor_, 0, static_cast<size_t>(
      reinterpret_cast<char*>(&versionmajor_) -
      reinterpret_cast<char*>(&versionminor_)) + sizeof(versionmajor_));
  _internal_metadata_.Clear<::PROTOBUF_NAMESPACE_ID::UnknownFieldSet>();
}

const char* ServerInfoReply::_InternalParse(const char* ptr, ::PROTOBUF_NAMESPACE_ID::internal::ParseContext* ctx) {
#define CHK_(x) if (PROTOBUF_PREDICT_FALSE(!(x))) goto failure
  while (!ctx->Done(&ptr)) {
    ::PROTOBUF_NAMESPACE_ID::uint32 tag;
    ptr = ::PROTOBUF_NAMESPACE_ID::internal::ReadTag(ptr, &tag);
    CHK_(ptr);
    switch (tag >> 3) {
      // string ServerName = 1;
      case 1:
        if (PROTOBUF_PREDICT_TRUE(static_cast<::PROTOBUF_NAMESPACE_ID::uint8>(tag) == 10)) {
          auto str = _internal_mutable_servername();
          ptr = ::PROTOBUF_NAMESPACE_ID::internal::InlineGreedyStringParser(str, ptr, ctx);
          CHK_(::PROTOBUF_NAMESPACE_ID::internal::VerifyUTF8(str, "clima_server.ServerInfoReply.ServerName"));
          CHK_(ptr);
        } else goto handle_unusual;
        continue;
      // int32 VersionMinor = 2;
      case 2:
        if (PROTOBUF_PREDICT_TRUE(static_cast<::PROTOBUF_NAMESPACE_ID::uint8>(tag) == 16)) {
          versionminor_ = ::PROTOBUF_NAMESPACE_ID::internal::ReadVarint64(&ptr);
          CHK_(ptr);
        } else goto handle_unusual;
        continue;
      // int32 VersionMajor = 3;
      case 3:
        if (PROTOBUF_PREDICT_TRUE(static_cast<::PROTOBUF_NAMESPACE_ID::uint8>(tag) == 24)) {
          versionmajor_ = ::PROTOBUF_NAMESPACE_ID::internal::ReadVarint64(&ptr);
          CHK_(ptr);
        } else goto handle_unusual;
        continue;
      default: {
      handle_unusual:
        if ((tag & 7) == 4 || tag == 0) {
          ctx->SetLastTag(tag);
          goto success;
        }
        ptr = UnknownFieldParse(tag,
            _internal_metadata_.mutable_unknown_fields<::PROTOBUF_NAMESPACE_ID::UnknownFieldSet>(),
            ptr, ctx);
        CHK_(ptr != nullptr);
        continue;
      }
    }  // switch
  }  // while
success:
  return ptr;
failure:
  ptr = nullptr;
  goto success;
#undef CHK_
}

::PROTOBUF_NAMESPACE_ID::uint8* ServerInfoReply::_InternalSerialize(
    ::PROTOBUF_NAMESPACE_ID::uint8* target, ::PROTOBUF_NAMESPACE_ID::io::EpsCopyOutputStream* stream) const {
  // @@protoc_insertion_point(serialize_to_array_start:clima_server.ServerInfoReply)
  ::PROTOBUF_NAMESPACE_ID::uint32 cached_has_bits = 0;
  (void) cached_has_bits;

  // string ServerName = 1;
  if (this->servername().size() > 0) {
    ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::VerifyUtf8String(
      this->_internal_servername().data(), static_cast<int>(this->_internal_servername().length()),
      ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::SERIALIZE,
      "clima_server.ServerInfoReply.ServerName");
    target = stream->WriteStringMaybeAliased(
        1, this->_internal_servername(), target);
  }

  // int32 VersionMinor = 2;
  if (this->versionminor() != 0) {
    target = stream->EnsureSpace(target);
    target = ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::WriteInt32ToArray(2, this->_internal_versionminor(), target);
  }

  // int32 VersionMajor = 3;
  if (this->versionmajor() != 0) {
    target = stream->EnsureSpace(target);
    target = ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::WriteInt32ToArray(3, this->_internal_versionmajor(), target);
  }

  if (PROTOBUF_PREDICT_FALSE(_internal_metadata_.have_unknown_fields())) {
    target = ::PROTOBUF_NAMESPACE_ID::internal::WireFormat::InternalSerializeUnknownFieldsToArray(
        _internal_metadata_.unknown_fields<::PROTOBUF_NAMESPACE_ID::UnknownFieldSet>(::PROTOBUF_NAMESPACE_ID::UnknownFieldSet::default_instance), target, stream);
  }
  // @@protoc_insertion_point(serialize_to_array_end:clima_server.ServerInfoReply)
  return target;
}

size_t ServerInfoReply::ByteSizeLong() const {
// @@protoc_insertion_point(message_byte_size_start:clima_server.ServerInfoReply)
  size_t total_size = 0;

  ::PROTOBUF_NAMESPACE_ID::uint32 cached_has_bits = 0;
  // Prevent compiler warnings about cached_has_bits being unused
  (void) cached_has_bits;

  // string ServerName = 1;
  if (this->servername().size() > 0) {
    total_size += 1 +
      ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::StringSize(
        this->_internal_servername());
  }

  // int32 VersionMinor = 2;
  if (this->versionminor() != 0) {
    total_size += 1 +
      ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::Int32Size(
        this->_internal_versionminor());
  }

  // int32 VersionMajor = 3;
  if (this->versionmajor() != 0) {
    total_size += 1 +
      ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::Int32Size(
        this->_internal_versionmajor());
  }

  if (PROTOBUF_PREDICT_FALSE(_internal_metadata_.have_unknown_fields())) {
    return ::PROTOBUF_NAMESPACE_ID::internal::ComputeUnknownFieldsSize(
        _internal_metadata_, total_size, &_cached_size_);
  }
  int cached_size = ::PROTOBUF_NAMESPACE_ID::internal::ToCachedSize(total_size);
  SetCachedSize(cached_size);
  return total_size;
}

void ServerInfoReply::MergeFrom(const ::PROTOBUF_NAMESPACE_ID::Message& from) {
// @@protoc_insertion_point(generalized_merge_from_start:clima_server.ServerInfoReply)
  GOOGLE_DCHECK_NE(&from, this);
  const ServerInfoReply* source =
      ::PROTOBUF_NAMESPACE_ID::DynamicCastToGenerated<ServerInfoReply>(
          &from);
  if (source == nullptr) {
  // @@protoc_insertion_point(generalized_merge_from_cast_fail:clima_server.ServerInfoReply)
    ::PROTOBUF_NAMESPACE_ID::internal::ReflectionOps::Merge(from, this);
  } else {
  // @@protoc_insertion_point(generalized_merge_from_cast_success:clima_server.ServerInfoReply)
    MergeFrom(*source);
  }
}

void ServerInfoReply::MergeFrom(const ServerInfoReply& from) {
// @@protoc_insertion_point(class_specific_merge_from_start:clima_server.ServerInfoReply)
  GOOGLE_DCHECK_NE(&from, this);
  _internal_metadata_.MergeFrom<::PROTOBUF_NAMESPACE_ID::UnknownFieldSet>(from._internal_metadata_);
  ::PROTOBUF_NAMESPACE_ID::uint32 cached_has_bits = 0;
  (void) cached_has_bits;

  if (from.servername().size() > 0) {
    _internal_set_servername(from._internal_servername());
  }
  if (from.versionminor() != 0) {
    _internal_set_versionminor(from._internal_versionminor());
  }
  if (from.versionmajor() != 0) {
    _internal_set_versionmajor(from._internal_versionmajor());
  }
}

void ServerInfoReply::CopyFrom(const ::PROTOBUF_NAMESPACE_ID::Message& from) {
// @@protoc_insertion_point(generalized_copy_from_start:clima_server.ServerInfoReply)
  if (&from == this) return;
  Clear();
  MergeFrom(from);
}

void ServerInfoReply::CopyFrom(const ServerInfoReply& from) {
// @@protoc_insertion_point(class_specific_copy_from_start:clima_server.ServerInfoReply)
  if (&from == this) return;
  Clear();
  MergeFrom(from);
}

bool ServerInfoReply::IsInitialized() const {
  return true;
}

void ServerInfoReply::InternalSwap(ServerInfoReply* other) {
  using std::swap;
  _internal_metadata_.Swap<::PROTOBUF_NAMESPACE_ID::UnknownFieldSet>(&other->_internal_metadata_);
  servername_.Swap(&other->servername_, &::PROTOBUF_NAMESPACE_ID::internal::GetEmptyStringAlreadyInited(), GetArena());
  ::PROTOBUF_NAMESPACE_ID::internal::memswap<
      PROTOBUF_FIELD_OFFSET(ServerInfoReply, versionmajor_)
      + sizeof(ServerInfoReply::versionmajor_)
      - PROTOBUF_FIELD_OFFSET(ServerInfoReply, versionminor_)>(
          reinterpret_cast<char*>(&versionminor_),
          reinterpret_cast<char*>(&other->versionminor_));
}

::PROTOBUF_NAMESPACE_ID::Metadata ServerInfoReply::GetMetadata() const {
  return GetMetadataStatic();
}


// @@protoc_insertion_point(namespace_scope)
}  // namespace clima_server
PROTOBUF_NAMESPACE_OPEN
template<> PROTOBUF_NOINLINE ::clima_server::ServerInfoRequest* Arena::CreateMaybeMessage< ::clima_server::ServerInfoRequest >(Arena* arena) {
  return Arena::CreateMessageInternal< ::clima_server::ServerInfoRequest >(arena);
}
template<> PROTOBUF_NOINLINE ::clima_server::ServerInfoReply* Arena::CreateMaybeMessage< ::clima_server::ServerInfoReply >(Arena* arena) {
  return Arena::CreateMessageInternal< ::clima_server::ServerInfoReply >(arena);
}
PROTOBUF_NAMESPACE_CLOSE

// @@protoc_insertion_point(global_scope)
#include <google/protobuf/port_undef.inc>
