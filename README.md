# VSIXTorch
It helps deverlopers to setup [PyTorch C++ Project](https://pytorch.org/cppdocs/installing.html) on Windows without CMake.

# Visual Studio Extension Download Link
[LibTorch Project Template](https://marketplace.visualstudio.com/items?itemName=YiZhang.LibTorch001)

# Libtorch Download Link
Pytorch.org only provide the latest libtorch binary links.
Generally, the libtorch download links

Debug <br>
https://download.pytorch.org/libtorch/{cuda-version}/libtorch-win-shared-with-deps-debug-{pytorch-version}%2B{cuversion}.zip

Release <br>
https://download.pytorch.org/libtorch/{cuda-version}/libtorch-win-shared-with-deps{pytorch-version}%2B{cuversion}.zip

For example
Libtorch {1.11.0 cu113 debug} download link is <br>
https://download.pytorch.org/libtorch/cu113/libtorch-win-shared-with-deps-debug-1.11.0%2Bcu113.zip<br>
If it's a cpu version, the {cuda-version} is cpu

# How to build dll
1. Change `Project settings->Configuration Properties->General->Configuration Type` to `Dynamic Library(.dll)`
2. Change `Project settings->Configuration Properties->Advanced->Target File Extension` to `dll`

## example
```C++
#include <torch/torch.h>
extern "C" __declspec(dllexport) int check_cuda() 
{
    if (torch::cuda::is_available()) {
        return 1;
    }
    else
    {
        return 0;
    }
}
```

# Tutorial Video
https://ossci-windows.s3.us-east-1.amazonaws.com/vsextension/demo.mp4)
