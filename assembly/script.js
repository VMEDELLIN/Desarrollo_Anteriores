let wasmExports=null;
let wasmMemory=new WebAssembly.Memory({initial:256, maximum:256});
let wasmTable=new WebAssembly.Table({
    'initial':1,
    'maximun':1,
    'element':'anyfunc'
});
let asmLibraryArg={
"_handle_stack_overflow":()=>{},
"emscripten_resize_heap":()=>{},
"_lock":()=>{},
"_unlock":()=>{},
"memory":wasmMemory,
"table":wasmTable
};
var info = {
    'env': asmLibraryArg,
    'wasi_snapshot_preview1': asmLibraryArg,
  };
async function loadWasm(){
    let reponse=await fetch('functions.wasm');
    let bytes=await reponse.arrayBuffer();
    let wasmObj=await WebAssembly.instantiate(bytes,info);
    wasmExports=wasmObj.instance.exports;
};
loadWasm();