using Assignment3;
using Assignment3.TestCase1;
using Assignment3.TestCase2;
using System.Collections.Generic;
using System.Numerics;

ApplicationDbContext context = new();

var orm1 = new MyORM<Guid, Item>();
var orm2 = new MyORM<Guid, Product>();
var orm3 = new MyORM<Guid, Vendor>();
var orm4 = new MyORM<Guid, TestClass1>();
var orm5 = new MyORM<Guid, TestClass2>();


#region TestCase1

//Insert Item
var colorWhite = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#FF5733", Name = "White" };
var colorRed = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#FF0000", Name = "Red" };
var colorBlue = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#0000FF", Name = "Blue" };
var colorGreen = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#008000", Name = "Green" };

var user1 = new User { Id = IdentityGenerator.NewSequentialGuid(), Name = "Test User 1", Email = "testuser1@email.com" };
var user2 = new User { Id = IdentityGenerator.NewSequentialGuid(), Name = "Test User 2", Email = "testuser2@email.com" };
var user3 = new User { Id = IdentityGenerator.NewSequentialGuid(), Name = "Test User 3", Email = "testuser3@email.com" };

var feedbackGood = new Feedback { Id = IdentityGenerator.NewSequentialGuid(), FeedbackGiver = user1, Rating = 4.5, Comment = "Good feedback comment" };
var feedbackMedium = new Feedback { Id = IdentityGenerator.NewSequentialGuid(), FeedbackGiver = user2, Rating = 3, Comment = "Medium feedback comment" };
var feedbackBad = new Feedback { Id = IdentityGenerator.NewSequentialGuid(), FeedbackGiver = user3, Rating = 1, Comment = "Bad feedback comment" };

var item = new Item
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Colors = new List<Color> { colorWhite, colorBlue, colorRed, colorGreen },
    Feedbacks = new List<Feedback> { feedbackGood, feedbackMedium, feedbackBad },
    Description = "Test item 1 description...",
    Name = "Test Item 1",
    PhotoUrl = "https://testimages.s3.ap-southeast-1.amazonaws.com/img1"
};
orm1.Insert(item);


//Update Item
colorWhite = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#FF5733", Name = "White" };
colorRed = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#FF0000", Name = "Red" };
colorBlue = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#0000FF", Name = "Blue" };
colorGreen = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#008000", Name = "Green" };

var updatedColorWhite = new Color { Id = colorWhite.Id, Code = "#FFDEAD", Name = "Navajo White" };
var updatedColorRed = new Color { Id = colorRed.Id, Code = "#EE4B2B", Name = "Bright Red" };
var updatedColorBlue = new Color { Id = colorBlue.Id, Code = "#00008B", Name = "Dark Blue" };
var updatedColorGreen = new Color { Id = colorGreen.Id, Code = "#0FFF50", Name = "Neon Green" };

user1 = new User { Id = IdentityGenerator.NewSequentialGuid(), Name = "Test User 1", Email = "testuser1@email.com" };
user2 = new User { Id = IdentityGenerator.NewSequentialGuid(), Name = "Test User 2", Email = "testuser2@email.com" };
user3 = new User { Id = IdentityGenerator.NewSequentialGuid(), Name = "Test User 3", Email = "testuser3@email.com" };

var updatedUser1 = new User { Id = user1.Id, Name = "Updated Test User 1", Email = "updatedtestuser1@email.com" };
var updatedUser2 = new User { Id = user2.Id, Name = "Updated Test User 2", Email = "updatedtestuser2@email.com" };
var updatedUser3 = new User { Id = user3.Id, Name = "Updated Test User 3", Email = "updatedtestuser3@email.com" };

feedbackGood = new Feedback { Id = IdentityGenerator.NewSequentialGuid(), FeedbackGiver = user1, Rating = 4.5, Comment = "Good feedback comment" };
feedbackMedium = new Feedback { Id = IdentityGenerator.NewSequentialGuid(), FeedbackGiver = user2, Rating = 3, Comment = "Medium feedback comment" };
feedbackBad = new Feedback { Id = IdentityGenerator.NewSequentialGuid(), FeedbackGiver = user3, Rating = 1, Comment = "Bad feedback comment" };

var updatedFeedbackGood = new Feedback { Id = feedbackGood.Id, FeedbackGiver = updatedUser1, Rating = 5, Comment = "Updated Good feedback comment" };
var updatedFeedbackMedium = new Feedback { Id = feedbackMedium.Id, FeedbackGiver = updatedUser2, Rating = 3.5, Comment = "Updated Medium feedback comment" };
var updatedFeedbackBad = new Feedback { Id = feedbackBad.Id, FeedbackGiver = updatedUser3, Rating = 1.5, Comment = "Updated Bad feedback comment" };

item = new Item
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Colors = new List<Color> { colorWhite, colorBlue, colorRed, colorGreen },
    Feedbacks = new List<Feedback> { updatedFeedbackGood, feedbackMedium, feedbackBad },
    Description = "Test item 1 description...",
    Name = "Test Item 1",
    PhotoUrl = "https://testimages.s3.ap-southeast-1.amazonaws.com/img1"
};

var itemForUpdate = new Item
{
    Id = item.Id,
    Colors = new List<Color> { updatedColorWhite, updatedColorRed, updatedColorBlue, updatedColorGreen },
    Feedbacks = new List<Feedback> { updatedFeedbackGood, updatedFeedbackMedium, updatedFeedbackBad },
    Description = "Updated Test item 1 description...",
    Name = "Updated Test Item 1",
    PhotoUrl = "https://updatedtestimages.s3.ap-southeast-1.amazonaws.com/img1"
};

orm1.Insert(item);
orm1.Update(itemForUpdate);


//Delete Item
colorWhite = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#FF5733", Name = "White" };
colorBlue = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#0000FF", Name = "Blue" };
colorRed = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#FF0000", Name = "Red" };
colorGreen = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#008000", Name = "Green" };

user1 = new User { Id = IdentityGenerator.NewSequentialGuid(), Name = "Test User 1", Email = "testuser1@email.com" };
user2 = new User { Id = IdentityGenerator.NewSequentialGuid(), Name = "Test User 2", Email = "testuser2@email.com" };
user3 = new User { Id = IdentityGenerator.NewSequentialGuid(), Name = "Test User 3", Email = "testuser3@email.com" };

feedbackGood = new Feedback { Id = IdentityGenerator.NewSequentialGuid(), FeedbackGiver = user1, Rating = 4.5, Comment = "Good feedback comment" };
feedbackMedium = new Feedback { Id = IdentityGenerator.NewSequentialGuid(), FeedbackGiver = user2, Rating = 3, Comment = "Medium feedback comment" };
feedbackBad = new Feedback { Id = IdentityGenerator.NewSequentialGuid(), FeedbackGiver = user3, Rating = 1, Comment = "Bad feedback comment" };

item = new Item
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Colors = new List<Color> { colorWhite, colorBlue, colorRed, colorGreen },
    Feedbacks = new List<Feedback> { feedbackGood, feedbackMedium, feedbackBad },
    Description = "Test item 1 description...",
    Name = "Test Item 1",
    PhotoUrl = "https://testimages.s3.ap-southeast-1.amazonaws.com/img1"
};

orm1.Insert(item);
orm1.Delete(item);


//Insert Product
colorWhite = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#FF5733", Name = "White" };
colorRed = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#FF0000", Name = "Red" };
colorBlue = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#0000FF", Name = "Blue" };
colorGreen = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#008000", Name = "Green" };

user1 = new User { Id = IdentityGenerator.NewSequentialGuid(), Name = "Test User 1", Email = "testuser1@email.com" };
user2 = new User { Id = IdentityGenerator.NewSequentialGuid(), Name = "Test User 2", Email = "testuser2@email.com" };
user3 = new User { Id = IdentityGenerator.NewSequentialGuid(), Name = "Test User 3", Email = "testuser3@email.com" };

feedbackGood = new Feedback { Id = IdentityGenerator.NewSequentialGuid(), FeedbackGiver = user1, Rating = 4.5, Comment = "Good feedback comment" };
feedbackMedium = new Feedback { Id = IdentityGenerator.NewSequentialGuid(), FeedbackGiver = user2, Rating = 3, Comment = "Medium feedback comment" };
feedbackBad = new Feedback { Id = IdentityGenerator.NewSequentialGuid(), FeedbackGiver = user3, Rating = 1, Comment = "Bad feedback comment" };

var product = new Product
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Name = "Test Product 1",
    Price = 50000,
    Colors = new List<Color> { colorWhite, colorBlue, colorRed, colorGreen },
    Feedbacks = new List<Feedback> { feedbackGood, feedbackMedium, feedbackBad }
};

orm2.Insert(product);


//Update Product
colorWhite = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#FF5733", Name = "White" };
colorRed = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#FF0000", Name = "Red" };
colorBlue = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#0000FF", Name = "Blue" };
colorGreen = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#008000", Name = "Green" };

updatedColorWhite = new Color { Id = colorWhite.Id, Code = "#FFDEAD", Name = "Navajo White" };
updatedColorRed = new Color { Id = colorRed.Id, Code = "#EE4B2B", Name = "Bright Red" };
updatedColorBlue = new Color { Id = colorBlue.Id, Code = "#00008B", Name = "Dark Blue" };
updatedColorGreen = new Color { Id = colorGreen.Id, Code = "#0FFF50", Name = "Neon Green" };

user1 = new User { Id = IdentityGenerator.NewSequentialGuid(), Name = "Test User 1", Email = "testuser1@email.com" };
user2 = new User { Id = IdentityGenerator.NewSequentialGuid(), Name = "Test User 2", Email = "testuser2@email.com" };
user3 = new User { Id = IdentityGenerator.NewSequentialGuid(), Name = "Test User 3", Email = "testuser3@email.com" };

updatedUser1 = new User { Id = user1.Id, Name = "Updated Test User 1", Email = "updatedtestuser1@email.com" };
updatedUser2 = new User { Id = user2.Id, Name = "Updated Test User 2", Email = "updatedtestuser2@email.com" };
updatedUser3 = new User { Id = user3.Id, Name = "Updated Test User 3", Email = "updatedtestuser3@email.com" };

feedbackGood = new Feedback { Id = IdentityGenerator.NewSequentialGuid(), FeedbackGiver = user1, Rating = 4.5, Comment = "Good feedback comment" };
feedbackMedium = new Feedback { Id = IdentityGenerator.NewSequentialGuid(), FeedbackGiver = user2, Rating = 3, Comment = "Medium feedback comment" };
feedbackBad = new Feedback { Id = IdentityGenerator.NewSequentialGuid(), FeedbackGiver = user3, Rating = 1, Comment = "Bad feedback comment" };

updatedFeedbackGood = new Feedback { Id = feedbackGood.Id, FeedbackGiver = updatedUser1, Rating = 5, Comment = "Updated Good feedback comment" };
updatedFeedbackMedium = new Feedback { Id = feedbackMedium.Id, FeedbackGiver = updatedUser2, Rating = 3.5, Comment = "Updated Medium feedback comment" };
updatedFeedbackBad = new Feedback { Id = feedbackBad.Id, FeedbackGiver = updatedUser3, Rating = 1.5, Comment = "Updated Bad feedback comment" };

product = new Product
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Name = "Test Product 1",
    Price = 70000,
    Colors = new List<Color> { colorWhite, colorRed, colorBlue, colorGreen },
    Feedbacks = new List<Feedback> { feedbackGood, feedbackMedium, feedbackBad }
};

var productForUpdate = new Product
{
    Id = product.Id,
    Name = "Updated Test Product 1",
    Price = 100000,
    Colors = new List<Color> { updatedColorWhite, updatedColorRed, updatedColorBlue, updatedColorGreen },
    Feedbacks = new List<Feedback> { updatedFeedbackGood, updatedFeedbackMedium, updatedFeedbackBad }
};

orm2.Insert(product);
orm2.Update(productForUpdate);


//Delete Product
colorWhite = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#FF5733", Name = "White" };
colorBlue = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#0000FF", Name = "Blue" };
colorGreen = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#008000", Name = "Green" };
colorRed = new Color { Id = IdentityGenerator.NewSequentialGuid(), Code = "#FF0000", Name = "Red" };

user1 = new User { Id = IdentityGenerator.NewSequentialGuid(), Name = "Test User 1", Email = "testuser1@email.com" };
user2 = new User { Id = IdentityGenerator.NewSequentialGuid(), Name = "Test User 2", Email = "testuser2@email.com" };
user3 = new User { Id = IdentityGenerator.NewSequentialGuid(), Name = "Test User 3", Email = "testuser3@email.com" };

feedbackGood = new Feedback { Id = IdentityGenerator.NewSequentialGuid(), FeedbackGiver = user1, Rating = 4.5, Comment = "Good feedback comment" };
feedbackMedium = new Feedback { Id = IdentityGenerator.NewSequentialGuid(), FeedbackGiver = user2, Rating = 3, Comment = "Medium feedback comment" };
feedbackBad = new Feedback { Id = IdentityGenerator.NewSequentialGuid(), FeedbackGiver = user3, Rating = 1, Comment = "Bad feedback comment" };

product = new Product
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Name = "Test Product 1",
    Price = 10000,
    Colors = new List<Color> { colorWhite, colorBlue, colorGreen, colorRed },
    Feedbacks = new List<Feedback> { feedbackGood, feedbackMedium, feedbackBad }
};

orm2.Insert(product);
orm2.Delete(product);


//Insert Vendor
var vendor = new Vendor
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Name = "Test Vendor 1",
    Enlisted = true
};

orm3.Insert(vendor);


//Update Vendor
vendor = new Vendor
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Name = "Test Vendor 1",
    Enlisted = true
};

var vendorForUpdate = new Vendor
{
    Id = vendor.Id,
    Name = "Updated Vendor",
    Enlisted = false
};

orm3.Insert(vendor);
orm3.Update(vendorForUpdate);


//Delete Vendor
vendor = new Vendor
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Name = "Test Vendor 1",
    Enlisted = true
};

orm3.Insert(vendor);
orm3.Delete(vendor);

#endregion


#region TestCase2

//Insert TestClass1
var subClass2 = new SubClass2
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Property1 = "Test Property1",
    Property2 = "Test Property2",
};

var temp31 = new Temp3 { Id = IdentityGenerator.NewSequentialGuid(), Name = "Temp 3 1" };
var temp32 = new Temp3 { Id = IdentityGenerator.NewSequentialGuid(), Name = "Temp 3 2" };

var temp21 = new Temp2 { Id = IdentityGenerator.NewSequentialGuid(), Temp = temp31 };
var temp22 = new Temp2 { Id = IdentityGenerator.NewSequentialGuid(), Temp = temp32 };
var temp23 = new Temp2 { Id = IdentityGenerator.NewSequentialGuid() };

var temp11 = new Temp1 { Id = IdentityGenerator.NewSequentialGuid(), Temps = new List<Temp2> { temp21, temp22 } };
var temp12 = new Temp1 { Id = IdentityGenerator.NewSequentialGuid(), Temps = new List<Temp2> { temp23 } };
var temp13 = new Temp1 { Id = IdentityGenerator.NewSequentialGuid() };

var testClass1 = new TestClass1
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Class2 = subClass2,
    Temps = new List<Temp1> { temp11, temp12, temp13 }
};

orm4.Insert(testClass1);


//Update TestClass1
subClass2 = new SubClass2
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Property1 = "Test Property1",
    Property2 = "Test Property2",
};

var updatedSubClass2 = new SubClass2
{
    Id = subClass2.Id,
    Property1 = "New Test Property1",
    Property2 = "New Test Property2",
};

temp31 = new Temp3 { Id = IdentityGenerator.NewSequentialGuid(), Name = "Temp 3 1" };
temp32 = new Temp3 { Id = IdentityGenerator.NewSequentialGuid(), Name = "Temp 3 2" };

var updatedTemp31 = new Temp3 { Id = temp31.Id, Name = "New Temp 3 1" };
var updatedTemp32 = new Temp3 { Id = temp32.Id, Name = "New Temp 3 2" };

temp21 = new Temp2 { Id = IdentityGenerator.NewSequentialGuid(), Temp = temp31 };
temp22 = new Temp2 { Id = IdentityGenerator.NewSequentialGuid(), Temp = temp32 };
temp23 = new Temp2 { Id = IdentityGenerator.NewSequentialGuid() };

var updatedTemp21 = new Temp2 { Id = temp21.Id, Temp = updatedTemp31 };
var updatedTemp22 = new Temp2 { Id = temp22.Id, Temp = updatedTemp32 };
var updatedTemp23 = new Temp2 { Id = temp23.Id };

temp11 = new Temp1 { Id = IdentityGenerator.NewSequentialGuid(), Temps = new List<Temp2> { temp21, temp22 } };
temp12 = new Temp1 { Id = IdentityGenerator.NewSequentialGuid(), Temps = new List<Temp2> { temp23 } };
temp13 = new Temp1 { Id = IdentityGenerator.NewSequentialGuid() };

var updatedTemp11 = new Temp1 { Id = temp11.Id, Temps = new List<Temp2> { updatedTemp21, updatedTemp22 } };
var updatedTemp12 = new Temp1 { Id = temp12.Id, Temps = new List<Temp2> { updatedTemp23 } };
var updatedTemp13 = new Temp1 { Id = temp13.Id };

testClass1 = new TestClass1
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Class2 = subClass2,
    Temps = new List<Temp1> { temp11, temp12, temp13 }
};

var testClass1ForUpdate = new TestClass1
{
    Id = testClass1.Id,
    Class2 = updatedSubClass2,
    Temps = new List<Temp1> { updatedTemp11, updatedTemp12, updatedTemp13 }
};

orm4.Insert(testClass1);
orm4.Update(testClass1ForUpdate);


//Delete TestClass1
subClass2 = new SubClass2
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Property1 = "Test Property1",
    Property2 = "Test Property2",
};

temp31 = new Temp3 { Id = IdentityGenerator.NewSequentialGuid(), Name = "Temp 3 1" };
temp32 = new Temp3 { Id = IdentityGenerator.NewSequentialGuid(), Name = "Temp 3 2" };

temp21 = new Temp2 { Id = IdentityGenerator.NewSequentialGuid(), Temp = temp31 };
temp22 = new Temp2 { Id = IdentityGenerator.NewSequentialGuid(), Temp = temp32 };
temp23 = new Temp2 { Id = IdentityGenerator.NewSequentialGuid() };

temp11 = new Temp1 { Id = IdentityGenerator.NewSequentialGuid(), Temps = new List<Temp2> { temp21, temp22 } };
temp12 = new Temp1 { Id = IdentityGenerator.NewSequentialGuid(), Temps = new List<Temp2> { temp23 } };
temp13 = new Temp1 { Id = IdentityGenerator.NewSequentialGuid() };

testClass1 = new TestClass1
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Class2 = subClass2,
    Temps = new List<Temp1> { temp11, temp12, temp13 }
};

orm4.Insert(testClass1);
orm4.Delete(testClass1);


//Insert TestClass2
var subClass1 = new SubClass1
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Property1 = "Test Property1",
    Property2 = "Test Property2",
};

var subClass21 = new SubClass2
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Property1 = "Test Property1",
    Property2 = "Test Property2",
};

var subClass22 = new SubClass2
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Property1 = "Test Property1",
    Property2 = "Test Property2",
};

temp31 = new Temp3 { Id = IdentityGenerator.NewSequentialGuid(), Name = "Temp 3 1" };
temp32 = new Temp3 { Id = IdentityGenerator.NewSequentialGuid(), Name = "Temp 3 2" };

temp21 = new Temp2 { Id = IdentityGenerator.NewSequentialGuid(), Temp = temp31 };
temp22 = new Temp2 { Id = IdentityGenerator.NewSequentialGuid(), Temp = temp32 };
temp23 = new Temp2 { Id = IdentityGenerator.NewSequentialGuid() };

temp11 = new Temp1 { Id = IdentityGenerator.NewSequentialGuid(), Temps = new List<Temp2> { temp21, temp22 } };
temp12 = new Temp1 { Id = IdentityGenerator.NewSequentialGuid(), Temps = new List<Temp2> { temp23 } };
temp13 = new Temp1 { Id = IdentityGenerator.NewSequentialGuid() };

var testClass2 = new TestClass2
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Class1 = subClass1,
    SubClasses = new List<SubClass2> { subClass21, subClass22 },
    Temps = new List<Temp1> { temp11, temp12, temp13 }
};

orm5.Insert(testClass2);


//Update TestClass2
subClass1 = new SubClass1
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Property1 = "Test Property1",
    Property2 = "Test Property2",
};

var updatedSubClass1 = new SubClass1
{
    Id = subClass1.Id,
    Property1 = "New Test Property1",
    Property2 = "New Test Property2",
};

subClass21 = new SubClass2
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Property1 = "Test Property1",
    Property2 = "Test Property2",
};

var updatedSubClass21 = new SubClass2
{
    Id = subClass21.Id,
    Property1 = "New Test Property1",
    Property2 = "New Test Property2",
};

subClass22 = new SubClass2
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Property1 = "Test Property1",
    Property2 = "Test Property2",
};

var updatedSubClass22 = new SubClass2
{
    Id = subClass22.Id,
    Property1 = "Test Property1",
    Property2 = "Test Property2",
};

temp31 = new Temp3 { Id = IdentityGenerator.NewSequentialGuid(), Name = "Temp 3 1" };
temp32 = new Temp3 { Id = IdentityGenerator.NewSequentialGuid(), Name = "Temp 3 2" };

updatedTemp31 = new Temp3 { Id = temp31.Id, Name = "New Temp 3 1" };
updatedTemp32 = new Temp3 { Id = temp32.Id, Name = "New Temp 3 2" };

temp21 = new Temp2 { Id = IdentityGenerator.NewSequentialGuid(), Temp = temp31 };
temp22 = new Temp2 { Id = IdentityGenerator.NewSequentialGuid(), Temp = temp32 };
temp23 = new Temp2 { Id = IdentityGenerator.NewSequentialGuid() };

updatedTemp21 = new Temp2 { Id = temp21.Id, Temp = updatedTemp31 };
updatedTemp22 = new Temp2 { Id = temp22.Id, Temp = updatedTemp32 };
updatedTemp23 = new Temp2 { Id = temp23.Id };

temp11 = new Temp1 { Id = IdentityGenerator.NewSequentialGuid(), Temps = new List<Temp2> { temp21, temp22 } };
temp12 = new Temp1 { Id = IdentityGenerator.NewSequentialGuid(), Temps = new List<Temp2> { temp23 } };
temp13 = new Temp1 { Id = IdentityGenerator.NewSequentialGuid() };

updatedTemp11 = new Temp1 { Id = temp11.Id, Temps = new List<Temp2> { updatedTemp21, updatedTemp22 } };
updatedTemp12 = new Temp1 { Id = temp12.Id, Temps = new List<Temp2> { updatedTemp23 } };
updatedTemp13 = new Temp1 { Id = temp13.Id };

testClass2 = new TestClass2
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Class1 = subClass1,
    SubClasses = new List<SubClass2> { subClass21, subClass22 },
    Temps = new List<Temp1> { temp11, temp12, temp13 }
};

var testClass2ForUpdate = new TestClass2
{
    Id = testClass2.Id,
    Class1 = updatedSubClass1,
    SubClasses = new List<SubClass2> { updatedSubClass21, updatedSubClass22 },
    Temps = new List<Temp1> { updatedTemp11, updatedTemp12, updatedTemp13 }
};

orm5.Insert(testClass2);
orm5.Update(testClass2ForUpdate);


//Delete TestClass2
subClass1 = new SubClass1
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Property1 = "Test Property1",
    Property2 = "Test Property2",
};

subClass21 = new SubClass2
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Property1 = "Test Property1",
    Property2 = "Test Property2",
};

subClass22 = new SubClass2
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Property1 = "Test Property1",
    Property2 = "Test Property2",
};

var subClass23 = new SubClass2
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Property1 = "Test Property1",
    Property2 = "Test Property2",
};

temp31 = new Temp3 { Id = IdentityGenerator.NewSequentialGuid(), Name = "Temp 3 1" };
temp32 = new Temp3 { Id = IdentityGenerator.NewSequentialGuid(), Name = "Temp 3 2" };

temp21 = new Temp2 { Id = IdentityGenerator.NewSequentialGuid(), Temp = temp31 };
temp22 = new Temp2 { Id = IdentityGenerator.NewSequentialGuid(), Temp = temp32 };
temp23 = new Temp2 { Id = IdentityGenerator.NewSequentialGuid() };

temp11 = new Temp1 { Id = IdentityGenerator.NewSequentialGuid(), Temps = new List<Temp2> { temp21, temp22 } };
temp12 = new Temp1 { Id = IdentityGenerator.NewSequentialGuid(), Temps = new List<Temp2> { temp23 } };
temp13 = new Temp1 { Id = IdentityGenerator.NewSequentialGuid() };

testClass2 = new TestClass2
{
    Id = IdentityGenerator.NewSequentialGuid(),
    Class1 = subClass1,
    SubClasses = new List<SubClass2> { subClass21, subClass22, subClass23 },
    Temps = new List<Temp1> { temp11, temp12, temp13 }
};

orm5.Insert(testClass2);
orm5.Delete(testClass2);

#endregion
