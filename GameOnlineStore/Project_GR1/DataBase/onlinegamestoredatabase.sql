drop database if exists OnlineGameStore;
create database if not exists OnlineGameStore;
use OnlineGameStore;

create table if not exists Users(
	user_ID int primary key not null,
    user_Name nvarchar(255),
    user_Email varchar(255), 
    user_PassWord varchar(255) not null,
    user_Balance double ,
    user_Phone varchar(255)
);
insert into users values(1,"tk01","manh@gmail.com","123456","12345678","1234567890");
insert into users value(2,"tk02","abckkk@gmail.com","123456","9999999","12345678890");
insert into users value(3,"dungpro","dunghoangcy@gmail.com","asd12345","9999999","0915842058");

create table if not exists Orders( 
	order_ID int primary key auto_increment,
    user_ID int,
    order_date datetime default now()
);

select * from orders ;

create table if not exists Items(
	item_ID int primary key not null,
	item_Name varchar(255),
	item_Price double,
    item_Description nvarchar(999),
    item_tag varchar(255),
    item_DEVELOPER varchar(255),
    item_PUBLISHER varchar(255),
    item_platform varchar(255)
);
insert into items 
values
(101,"Grand Thief Auto 5",999000,"Grand Theft Auto V là một trong những game được phát triển bởi hãng Rockstar Games. Là phiên bản thứ mười lăm trong dòng trò chơi Grand Theft Auto và là phần kế tiếp của Grand Theft Auto IV.","Action, Openworld","Rockstar North","Rockstar Games","Xbox,Microsoft Windows,PS4"),
(102,"ARK: Survival Evolved",369000,"ARK: Survival Evolved là thể loại game nhập vai sinh tồn đầy khắc nghiệt, mang tới cho người chơi phong cách chơi hoàn toàn tự do. Bạn có thể làm bất cứ những gì mình muốn trong thế giới này.","Action, Survival, Openworld","Studio Wildcard","Studio Wildcard","Xbox,Microsoft Windows,PS4"),
(103,"Left 4 Dead",113000,"Left 4 Dead là một trò chơi kinh dị hành động, đưa ra bốn người chơi trong một cuộc đấu tranh sinh tồn để chống lại lũ zombie tràn ngập và những con quái vật đột biến đáng sợ. ","Action,FPS","Valve","Valve","Xbox,OS X,Linux,Microsoft Windows"),
(104,"Terraria",113000,"Terraria là một Trò chơi độc lập dạng phiêu lưu và sinh tồn do Re-Logic tạo ra. Trò chơi lần đầu tiên được phát hành trên PC vào ngày 16 tháng 5 năm 2011.","2D,openworld,survival","Re-Logic","Re-Logic","Microsoft Windows,Android,iOS,PS 4,Xbox One,Nintendo,3DS,Wii U"),
(105,"The Forest",178000,"The Forest là một tựa game sinh tồn kinh dị nổi tiếng với cốt một cốt truyện hết sức lôi cuốn và hồi hộp. Người chơi sẽ phải sinh tồn để chống chọi lại sinh vật và bộ lạc để tồn tại.","Survival, Action, Horror","Endnight Games Ltd","Endnight Games Ltd","Microsoft Windows,PS4"),
(106,"7 Days to Die",250000," 7 Days to Die là một trò chơi video kinh dị sinh tồn lấy bối cảnh trong một thế giới mở được phát triển bởi The Fun Pimps.","Action, Indie, Adventure","The Fun Pimps","The Fun Pimps","Windows,PS4,Xbox"),
(107,"Mad Max",200000,"Mad Max là một trò chơi video phiêu lưu hành động dựa trên nhượng quyền thương mại Mad Max. Được phát triển bởi Avalanche Studios và được phát hành bởi Warner Bros.","Action, Adventure","Avalanche Studios, Feral Interactive","Warner Bros. Interactive Entertainment","Windows,PS4,Xbox"),
(108,"Dark Souls 3",1000000,"Với thành công vang dội từ 2 phần trước,Darksoul 3 sẽ hứa hẹn đem đến lại những điều mới mẻ hơn trải nghiệm tuyệt phẩm với lối chơi luôn cuốn và hâp dẫn","Action, RPG"," FromSoftware, Inc","Bandai Namco Entertainment","Windows,PS4,Xbox"),
(109,"Life Is Strange",200000,"Life Is Strange là một trò chơi điện tử phiêu lưu đồ họa nhiều tập được phát triển bởi Dontnod Entertainment và được phát hành bởi Square Enix cho Microsoft Windows, PlayStation 3, PlayStation 4, Xbox 360, và Xbox One. Năm tập của trò chơi được phát hành định kỳ thông suốt năm 2015. Cốt truyện của trò chơi tập trung vào Max Caulfield, một cô học sinh nhiếp ảnh 18 tuổi phát hiện ra rằng mình có khả năng quay ngược thời gian bất cứ lúc nào, đưa ra các lựa chọn khác nhau và gây ra hiệu ứng cánh bướm. Sau khi nhìn thấy một cơn bão sẽ đến trong tương lai, Max cần phải ngăn chặn nó phá hủy thị trấn cô đang sống. Các lựa chọn của người chơi sẽ ảnh hưởng đến các sự kiện sau đó, và có thể chọn lại khi có cơ hội quay ngược thời gian. Các nhiệm vụ thu thập và làm thay đổi bối cảnh trò chơi là những đặc điểm tiêu biểu của dòng trò chơi giải đố, dùng những cây đối thoại để trò chuyện và tìm kiếm thông tin","Adventure","Dontnod Entertainment","Square Enix,Feral Interactive","Android,Windows,PS4,Xbox");

create table if not exists OrderDetail(	
    order_ID int ,
    item_ID int
); 
select orders.order_id,user_id,item_id,order_date from orders inner join orderdetail on orderdetail.order_id where user_id = 1 group by order_id;

create table if not exists FeedBackUsers(
	Feedback_id int primary key auto_increment,
	user_ID int ,
    Item_ID int,
    Rate int,
    FeedBack nvarchar(9999)
);

select * from orderdetail where order_id = 1;
alter table FeedBackUsers add foreign key (user_ID) references Users(user_ID);
alter table orderdetail add primary key (order_id, item_id);
alter table Orders add foreign key (user_ID) references Users(user_ID);
alter table OrderDetail add foreign key (order_ID) references Orders(order_ID);
alter table OrderDetail add foreign key (item_ID) references Items(Item_ID);
alter table FeedBackUsers add foreign key (Item_ID) references Items(Item_ID);
