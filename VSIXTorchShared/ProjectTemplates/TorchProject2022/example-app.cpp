#include <torch/torch.h>
#include <iostream>

int main() {
  torch::Tensor tensor = torch::rand({2, 3});
  if (torch::cuda::is_available()) {
	  std::cout << "CUDA is available! Training on GPU" << std::endl;
	  auto tensor_cuda = tensor.cuda();
	  std::cout << tensor_cuda << std::endl;
  }
  else
  {
	  std::cout << "CUDA is not available! Training on CPU" << std::endl;
	  std::cout << tensor << std::endl;	  
  }

  std::cin.get();
}