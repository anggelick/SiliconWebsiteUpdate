using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Infrastructure.Services;

public class CourseService
{
    private readonly CourseRepository _courseRepository;
    private readonly CourseCategoryRepository _courseCategoryRepository;
    private readonly CourseAuthorRepository _courseAuthorRepository;
    private readonly ProfilePictureRepository _pictureRepository;
    private readonly UserProfileService _userProfileService;
    private readonly UserProfileRepository _userProfileRepository;
    private readonly SavedCoursesRepository _savedCoursesRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;

    public CourseService(CourseRepository courseRepository, CourseCategoryRepository courseCategoryRepository, CourseAuthorRepository courseAuthorRepository, ProfilePictureRepository pictureRepository, UserProfileService userProfileService, UserProfileRepository userProfileRepository, SavedCoursesRepository savedCoursesRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
    {
        _courseRepository = courseRepository;
        _courseCategoryRepository = courseCategoryRepository;
        _courseAuthorRepository = courseAuthorRepository;
        _pictureRepository = pictureRepository;
        _userProfileService = userProfileService;
        _userProfileRepository = userProfileRepository;
        _savedCoursesRepository = savedCoursesRepository;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public async Task RunAsync()
    {
        var result = await _courseRepository.GetAllAsync();
        if (result.Count() == 0)
        {
            await PopulateCourseTableAsync();
        }
    }

    public async Task PopulateCourseTableAsync()
    {
        List<CourseEntity> courses = new List<CourseEntity>
        {
            new CourseEntity
        {
            Name = "Fullstack Web Developer Course from Scratch",
            Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
            Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
            Price = 12.50m,
            HoursToComplete = 220,
            LikesPercentage = 94,
            LikesAmount = 4200,

            Image = new CourseImageEntity
            {
                ImageUrl = "/images/courses/course-1.png"
            },

            CourseAuthor = new CourseAuthorEntity
            {
                Name = "Albert Flores",
                Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                YoutubeFollowersQty = 240,
                FacebookFollowersQty = 180,

                Image = new CourseAuthorImageEntity
                {
                    ImageUrl = "/images/people/albert-flores.png"
                }
            },
            CourseCategory = new CourseCategoryEntity
            {
                Name = "Fullstack Development"
            }
        },

            new CourseEntity
        {
            Name = "HTML, CSS, JavaScript Web Developer",
            Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
            Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
            Price = 15.99m,
            HoursToComplete = 160,
            LikesPercentage = 92,
            LikesAmount = 3100,

            Image = new CourseImageEntity
            {
                ImageUrl = "WebbApp/wwwroot/images/courses/course-2.png"
            }, 

            CourseAuthor = new CourseAuthorEntity
            {
                Name = "Jenny Wilson & Marvin McKinney",
                Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                YoutubeFollowersQty = 240,
                FacebookFollowersQty = 180,

                Image = new CourseAuthorImageEntity
                {
                    ImageUrl = "/images/people/albert-flores.png"
                }
            },
            CourseCategory = new CourseCategoryEntity
            {
                Name = "Frontend Development"
            }
        },

            new CourseEntity
            {
                Name = "The Complete Front-End Web Development Course",
                Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
                Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
                Price = 9.99m,
                HoursToComplete = 100,
                LikesPercentage = 98,
                LikesAmount = 2700,

                Image = new CourseImageEntity
                {
                    ImageUrl = "/images/courses/course-3.png"
                },

                CourseAuthor = new CourseAuthorEntity
                {
                    Name = "Albert Flores",
                    Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                    YoutubeFollowersQty = 240,
                    FacebookFollowersQty = 180,

                    Image = new CourseAuthorImageEntity
                    {
                        ImageUrl = "/images/people/albert-flores.png"
                    }
                },
                CourseCategoryId = 2
        },

            new CourseEntity
        {
            Name = "iOS & Swift - The Complete iOS App Development Course",
            Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
            Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
            Price = 15.99m,
            HoursToComplete = 160,
            LikesPercentage = 92,
            LikesAmount = 3100,

            Image = new CourseImageEntity
            {
                ImageUrl = "/images/courses/course-4.png"
            },

            CourseAuthor = new CourseAuthorEntity
            {
                Name = "Marvin McKinney",
                Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                YoutubeFollowersQty = 240,
                FacebookFollowersQty = 180,

                Image = new CourseAuthorImageEntity
                {
                    ImageUrl = "/images/people/albert-flores.png"
                }
            },
            CourseCategory = new CourseCategoryEntity
            {
                Name = "App Development"
            }
        },

            new CourseEntity
        {
            Name = "Data Science & Machine Learning with Python",
            Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
            Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
            Price = 11.20m,
            HoursToComplete = 160,
            LikesPercentage = 92,
            LikesAmount = 3100,

            Image = new CourseImageEntity
            {
                ImageUrl = "/images/courses/course-5.png"
            },

            CourseAuthor = new CourseAuthorEntity
            {
                Name = "Esther Howard",
                Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                YoutubeFollowersQty = 240,
                FacebookFollowersQty = 180,

                Image = new CourseAuthorImageEntity
                {
                    ImageUrl = "/images/people/albert-flores.png"
                }
            },
            CourseCategory = new CourseCategoryEntity
            {
                Name = "AI Development"
            }
        },

            new CourseEntity
            {
                Name = "Creative CSS Drawing Course: Make Art With CSS",
                Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
                Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
                Price = 10.50m,
                HoursToComplete = 220,
                LikesPercentage = 94,
                LikesAmount = 4200,

                Image = new CourseImageEntity
                {
                    ImageUrl = "/images/courses/course-6.png"
                },

                CourseAuthor = new CourseAuthorEntity
                {
                    Name = "Robert Fox",
                    Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                    YoutubeFollowersQty = 240,
                    FacebookFollowersQty = 180,

                    Image = new CourseAuthorImageEntity
                    {
                        ImageUrl = "/images/people/albert-flores.png"
                    }
                },
                CourseCategoryId = 2
            },

            new CourseEntity
            {
                Name = "Blender Character Creator v2.0 for Video Games Design",
                Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
                Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
                Price = 18.99m,
                HoursToComplete = 160,
                LikesPercentage = 92,
                LikesAmount = 3100,

                Image = new CourseImageEntity
                {
                    ImageUrl = "/images/courses/course-7.png"
                },

                CourseAuthor = new CourseAuthorEntity
                {
                    Name = "Ralph Edwards",
                    Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                    YoutubeFollowersQty = 240,
                    FacebookFollowersQty = 180,

                    Image = new CourseAuthorImageEntity
                    {
                        ImageUrl = "/images/people/albert-flores.png"
                    }
                },
                CourseCategory = new CourseCategoryEntity
                {
                    Name = "Game Development"
                }
            },

            new CourseEntity
            {
                Name = "The Ultimate Guide to 2D Mobile Game Development with Unity",
                Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
                Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
                Price = 44.99m,
                HoursToComplete = 100,
                LikesPercentage = 98,
                LikesAmount = 2700,

                Image = new CourseImageEntity
                {
                    ImageUrl = "/images/courses/course-8.png"
                },

                CourseAuthor = new CourseAuthorEntity
                {
                    Name = "Albert Flores",
                    Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                    YoutubeFollowersQty = 240,
                    FacebookFollowersQty = 180,

                    Image = new CourseAuthorImageEntity
                    {
                        ImageUrl = "/images/people/albert-flores.png"
                    }
                },
                CourseCategoryId = 5
            },

            new CourseEntity
            {
                Name = "Learn JMETER from Scratch on Live Apps-Performance Testing",
                Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
                Ingress = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
                Price = 14.50m,
                HoursToComplete = 160,
                LikesPercentage = 92,
                LikesAmount = 3100,

                Image = new CourseImageEntity
                {
                    ImageUrl = "/images/courses/course-9.png"
                },

                CourseAuthor = new CourseAuthorEntity
                {
                    Name = "Jenny Wilson",
                    Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                    YoutubeFollowersQty = 240,
                    FacebookFollowersQty = 180,

                    Image = new CourseAuthorImageEntity
                    {
                        ImageUrl = "/images/people/albert-flores.png"
                    }
                },
                CourseCategoryId = 3
            },

            new CourseEntity
            {
                Name = "UI/UX Design for Beginners: Master the Art of User Interfaces",
                Description = "In this course, you will learn the core principles of UI/UX design, including user research, information architecture, interaction design, and visual design. You will also gain hands-on experience by creating your own user interfaces using popular design tools like Figma or Adobe XD.",
                Ingress = "Want to create user interfaces that are both beautiful and functional? This course will teach you the fundamentals of UI/UX design.",
                Price = 19.99m,
                HoursToComplete = 120,
                LikesPercentage = 90,
                LikesAmount = 1500,
                Image = new CourseImageEntity
                {
                    ImageUrl = "/images/courses/course-10.png"
                },
                CourseCategoryId = 2,

                CourseAuthor = new CourseAuthorEntity
                {
                    Name = "Jenny Wilson",
                    Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                    YoutubeFollowersQty = 240,
                    FacebookFollowersQty = 180,

                    Image = new CourseAuthorImageEntity
                    {
                        ImageUrl = "/images/people/albert-flores.png"
                    }
                },
            },

            new CourseEntity
            {
                Name = "Build APIs with Python and Flask: The Complete Guide",
                Description = "APIs (Application Programming Interfaces) are the building blocks of modern web applications. In this course, you will learn everything you need to know to build your own APIs using Python and the Flask microframework. You will cover topics such as routing, request handling, data validation, and authentication. By the end of this course, you will be able to build and deploy your own APIs that can be used by other applications.",
                Ingress = "Learn how to build powerful and scalable APIs using Python and the Flask microframework.",
                Price = 24.50m,
                HoursToComplete = 180,
                LikesPercentage = 95,
                LikesAmount = 2100,
                Image = new CourseImageEntity
                {
                    ImageUrl = "/images/courses/course-11.png"
                },
                CourseCategoryId = 4,

                CourseAuthor = new CourseAuthorEntity
                {
                    Name = "Jenny Wilson",
                    Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                    YoutubeFollowersQty = 240,
                    FacebookFollowersQty = 180,

                    Image = new CourseAuthorImageEntity
                    {
                        ImageUrl = "/images/people/albert-flores.png"
                    }
                },
            },

            new CourseEntity
            {
                Name = "The MERN Stack Developer Course: Master MongoDB, Express, React, and Node.js",
                Description = "The MERN stack (MongoDB, Express, React, and Node.js) is one of the most popular tech stacks for building web applications. In this course, you will learn each layer of the MERN stack and how they work together to create a full-fledged web application. You will build a real-world application throughout the course, giving you the opportunity to apply your newfound skills to a practical project.",
                Ingress = "Become a full-stack developer with the in-demand skills to build modern web applications.",
                Price = 29.99m,
                HoursToComplete = 220,
                LikesPercentage = 98,
                LikesAmount = 3800,
                Image = new CourseImageEntity
                {
                    ImageUrl = "/images/courses/course-12.png"
                },
                CourseCategoryId = 1,
                CourseAuthor = new CourseAuthorEntity
                {
                    Name = "Jenny Wilson",
                    Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                    YoutubeFollowersQty = 240,
                    FacebookFollowersQty = 180,

                    Image = new CourseAuthorImageEntity
                    {
                        ImageUrl = "/images/people/albert-flores.png"
                    }
                },
            },

            new CourseEntity
            {
                Name = "Natural Language Processing with Python: From Text to Meaning",
                Description = "Natural language processing (NLP) is a branch of artificial intelligence that deals with the interaction between computers and human language. In this course, you will learn the fundamentals of NLP and how to use Python libraries like NLTK and spaCy to perform tasks such as text cleaning, tokenization, stemming, lemmatization, sentiment analysis, and named entity recognition.",
                Ingress = "Unlock the power of natural language processing (NLP) and use Python to extract meaning from text data.",
                Price = 16.99m,
                HoursToComplete = 140,
                LikesPercentage = 93,
                LikesAmount = 2500,
                Image = new CourseImageEntity
                {
                    ImageUrl = "/images/courses/course-13.png"
                },
                CourseCategoryId = 4,
                CourseAuthor = new CourseAuthorEntity
                {
                    Name = "Jenny Wilson",
                    Description = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
                    YoutubeFollowersQty = 240,
                    FacebookFollowersQty = 180,

                    Image = new CourseAuthorImageEntity
                    {
                        ImageUrl = "/images/people/albert-flores.png"
                    }
                },
            },
        };

        foreach (var course in courses)
            await _courseRepository.CreateAsync(course);
    }

    public async Task<bool> SaveOrRemoveCourseAsync(int id, ClaimsPrincipal loggedInUser)
    {
        var user = await _userProfileService.GetLoggedInUserAsync(loggedInUser);
        if (user != null)
        {
            var savedCourse = new SavedCoursesEntity
            {
                UserProfileId = user.UserProfile.Id,
                CourseId = id
            };

            var exists = await _savedCoursesRepository.ExistsAsync(x => x.CourseId == savedCourse.CourseId && x.UserProfileId == savedCourse.UserProfileId);
            if (!exists)
            {
                user.UserProfile.SavedItems!.Add(savedCourse);
                await _userManager.UpdateAsync(user);
                return true;
            }
            else
            {
                await _savedCoursesRepository.DeleteAsync(x => x.CourseId == savedCourse.CourseId && x.UserProfileId == savedCourse.UserProfileId);
                return true;
            }
        }

        return false;
    }

    public async Task<List<SavedCoursesEntity>> GetSavedCoursesAsync(ClaimsPrincipal loggedInUser)
    {
        var user = await _userProfileService.GetLoggedInUserAsync(loggedInUser);

        if (user != null)
        {
            List<SavedCoursesEntity> savedCourses = [];

            foreach (var savedItem in user.UserProfile.SavedItems!)
            {
                savedCourses.Add(savedItem);
            }
            return savedCourses;
        }

        Console.WriteLine("error");
        return null!;
    }

    public async Task RemoveAllCoursesAssociatedWithUserAsync(ClaimsPrincipal loggedInUser)
    {
        var user = await _userProfileService.GetLoggedInUserAsync(loggedInUser);

        if (user.UserProfile.SavedItems != null)
        {
            foreach (var userCourse in user.UserProfile.SavedItems)
            {
                await _savedCoursesRepository.DeleteAsync(x => x.CourseId == userCourse.CourseId && x.UserProfileId == userCourse.UserProfileId);
            }
        }
    }    

    public async Task<CourseEntity> CreateCourseAsync(CourseEntity course)
    {
        var courseAuthor = course.CourseAuthor;
        var courseCategory = course.CourseCategory;

        #region AUTHOR CHECK

        if (courseAuthor != null)
        {
            var authorResult = await _courseAuthorRepository.ExistsAsync(x => 
                x.Name == courseAuthor.Name);

            switch (authorResult)
            {
                case true:
                    courseAuthor = await _courseAuthorRepository.GetOneAsync(x => x.Name == courseAuthor.Name);
                    course.CourseAuthorId = courseAuthor.Id;
                    course.CourseAuthor = null!;
                    break;

                case false:
                    var result = await _courseAuthorRepository.CreateAsync(courseAuthor);
                    course.CourseAuthorId = result.Id;
                    course.CourseAuthor = null!;
                    break;
            }
        }

        #endregion


        #region CATEGORY CHECK

        if (courseCategory != null)
        {
            var categoryResult = await _courseCategoryRepository.ExistsAsync(x => 
                x.Name == courseCategory.Name);

            switch (categoryResult)
            {
                case true:
                    courseCategory = await _courseCategoryRepository.GetOneAsync(x => x.Name == courseCategory.Name);
                    course.CourseCategoryId = courseCategory.Id;
                    course.CourseCategory = null!;

                    break;

                case false:
                    var result = await _courseCategoryRepository.CreateAsync(courseCategory);
                    course.CourseCategoryId = result.Id;
                    course.CourseCategory = null!;
                    break;
            }
        }

        #endregion

        var created = await _courseRepository.CreateAsync(course);

        return created;
    } 
}