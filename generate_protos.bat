
setlocal
set TOOLS_PATH=packages\Grpc.Tools.0.14.0\tools\windows_x86

%TOOLS_PATH%\protoc.exe -Ihelloshared/protos --csharp_out helloshared  helloshared/protos/helloworld.proto --grpc_out helloshared --plugin=protoc-gen-grpc=%TOOLS_PATH%\grpc_csharp_plugin.exe

endlocal