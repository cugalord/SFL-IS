using sfl.Data;
using sfl.Models;
using System;
using System.Linq;

namespace sfl.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CompanyContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            /*if (context.StaffRoles.Any())
            {
                return;   // DB has been seeded
            }*/

            if (!context.StaffRoles.Any())
            {
                var staffRoles = new StaffRole[]
                {
                new StaffRole{ID=0,Name="Warehouse worker"},
                new StaffRole{ID=1,Name="Warehouse manager"},
                new StaffRole{ID=2,Name="Delivery driver"},
                new StaffRole{ID=3,Name="International driver"},
                new StaffRole{ID=4,Name="Logistician"},
                new StaffRole{ID=5,Name="Admin"},
                };

                context.StaffRoles.AddRange(staffRoles);
                context.SaveChanges();
            }

            if (!context.Cities.Any())
            {
                var cities = new City[]
                {
                new City{Code="1000",Name="Ljubljana"},
                new City{Code="1310",Name="Ribnica"},
                new City{Code="1241",Name="Kamnik"},
                new City{Code="1330",Name="Kocevje"},
                };

                context.Cities.AddRange(cities);
                context.SaveChanges();
            }

            if (!context.Streets.Any())
            {
                var streets = new Street[]
                {
                    new Street{CityCode="1000", StreetName="Miklosiceva", StreetNumber=6},
                    new Street{CityCode="1310", StreetName="Knafljev trg", StreetNumber=2},
                    new Street{CityCode="1330", StreetName="Kidriceva", StreetNumber=8},
                };

                context.Streets.AddRange(streets);
                context.SaveChanges();
            }

            if (!context.Branches.Any())
            {
                var branches = new Branch[]
                {
                    new Branch{Name="Ljubljana branch",CityCode="1000", StreetName="Miklosiceva", StreetNumber=6},
                    new Branch{Name="Ribnica branch",CityCode="1310", StreetName="Knafljev trg", StreetNumber=2},
                };

                context.Branches.AddRange(branches);
                context.SaveChanges();
            }

            if (!context.ParcelStatuses.Any())
            {
                var statuses = new ParcelStatus[]
                {
                    new ParcelStatus{ID=0,Name="Active"},
                    new ParcelStatus{ID=1,Name="Waiting"},
                };

                context.ParcelStatuses.AddRange(statuses);
                context.SaveChanges();
            }

            if (!context.Parcels.Any())
            {
                var parcels = new Parcel[]
                {
                    new Parcel{Weight=0,Height=0,Width=0,Depth=0,ParcelStatusID=0,RecipientCode="1000",RecipientStreetName="Miklosiceva",RecipientStreetNumber=6,
                    SenderCode="1000",SenderStreetName="Miklosiceva",SenderStreetNumber=6},
                    new Parcel{Weight=0,Height=0,Width=0,Depth=0,ParcelStatusID=0,RecipientCode="1000",RecipientStreetName="Miklosiceva",RecipientStreetNumber=6,
                    SenderCode="1000",SenderStreetName="Miklosiceva",SenderStreetNumber=6},
                };

                context.Parcels.AddRange(parcels);
                context.SaveChanges();
            }

            if (!context.JobStatuses.Any())
            {
                var statuses = new JobStatus[]
                {
                    new JobStatus{ID=0, Name="Created"},
                    new JobStatus{ID=1, Name="Completed"},
                };

                context.JobStatuses.AddRange(statuses);
                context.SaveChanges();
            }

            if (!context.JobTypes.Any())
            {
                var types = new JobType[]
                {
                    new JobType{ID=0, Name="Handover"},
                    new JobType{ID=1, Name="Warehouse"},
                };

                context.JobTypes.AddRange(types);
                context.SaveChanges();
            }
            /*
            var courses = new Course[]
            {
                new Course{CourseID=1050,Title="Chemistry",Credits=3},
                new Course{CourseID=4022,Title="Microeconomics",Credits=3},
                new Course{CourseID=4041,Title="Macroeconomics",Credits=3},
                new Course{CourseID=1045,Title="Calculus",Credits=4},
                new Course{CourseID=3141,Title="Trigonometry",Credits=4},
                new Course{CourseID=2021,Title="Composition",Credits=3},
                new Course{CourseID=2042,Title="Literature",Credits=4}
            };

            context.Courses.AddRange(courses);
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
                new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
                new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
                new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
                new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
                new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
                new Enrollment{StudentID=3,CourseID=1050},
                new Enrollment{StudentID=4,CourseID=1050},
                new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
                new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
                new Enrollment{StudentID=6,CourseID=1045},
                new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            };

            context.Enrollments.AddRange(enrollments);*/
            //context.SaveChanges();
        }
    }
}