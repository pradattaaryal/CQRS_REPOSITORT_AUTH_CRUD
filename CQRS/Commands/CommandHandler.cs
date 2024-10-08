
using practices.Helpers;
using practices.Model;
using practices.Repositories;

namespace practices.CQRS.Commands
{
    public class CommandHandler
    {
        private readonly IProductRepository _productRepository;
        private readonly ImageHelper _imageHelper;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public CommandHandler(IProductRepository productRepository, ImageHelper imageHelper, IUserRepository userRepository, IConfiguration configuration)
        {
            _productRepository = productRepository;
            _imageHelper = imageHelper;
            _userRepository = userRepository;
            _configuration = configuration;
        }
        //for create handler
        public async Task Handlecreate(CreateProductCommand command)
        {
            var imagePath = await _imageHelper.SaveImageAsync(command.Image);
            var product = new Product
            {
                Name = command.Name,
                Price = command.Price,
                Image = imagePath
            };
            await _productRepository.AddProductAsync(product);
        }
        //for delete handler
        public async Task Handledelete(DeleteProductCommand command)
        {
            await _productRepository.DeleteProductAsync(command.Id);
        }
        //for update handler
        public async Task Handleupdate(UpdateProductCommand command)
        {
            var imagePath = await _imageHelper.SaveImageAsync(command.Image);
            var product = new Product
            {
                Id = command.Id,
                Name = command.Name,
                Price = command.Price,
                Image = imagePath
            };
            await _productRepository.UpdateProductAsync(product);
        }
        //for signup handler
        public async Task<string> HandleSignupAsync(SignupUserDto signupDto)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(signupDto.Email);
            if (existingUser != null)
                throw new Exception("User already exists.");

           

            var user = new User
            {
                Username = signupDto.Username,
                Email = signupDto.Email,
                Password  = signupDto.Password,
                Role = signupDto.Role
            };

            await _userRepository.AddUserAsync(user);

            return "User created successfully";
        }
        //for login handler
        public async Task<string> HandleLoginAsync(LoginUserDto loginDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null )
            {
                throw new Exception("Invalid login credentials.");
            }

            return JwtTokenGenerator.GenerateToken(user, _configuration);
        }
    }

}
