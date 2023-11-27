


-- ВАРІАНТ 10 Створити таблицю бази даних про список розсилання й передплатників: тема й зміст листа, дата
-- відправлення, імена й адреси передплатників, їхні облікові записи й паролі.


use SA2_Kyrylenko

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'mailing_list')
BEGIN
DROP TABLE mailing_list;

END;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'subscriber')
BEGIN
DROP TABLE subscriber;

END;



IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'subscriber')
BEGIN
    CREATE TABLE subscriber (
        id INT IDENTITY PRIMARY KEY,
        full_name VARCHAR(255) NOT NULL,
        address VARCHAR(255) NOT NULL,
        username VARCHAR(255) NOT NULL,
        password VARCHAR(255) NOT NULL
    );

    
    INSERT INTO subscriber(full_name, address, username, password) values
        ('John Doe', '123 Main St', 'john_doe', 'password123'),
        ('Jane Smith', '456 Oak Ave', 'jane_smith', 'pass456'),
        ('Bob Johnson', '789 Pine Ln', 'bob_johnson', 'secure789'),
        ('Alice Brown', '987 Elm Rd', 'alice_brown', 'password987'),
        ('Charlie White', '654 Birch Blvd', 'charlie_white', 'pass123'),
        ('Zoe Davis', '231 Cedar Dr', 'zoe_davis', 'password456'),
        ('David Lee', '876 Maple Ct', 'david_lee', 'pass789'),
        ('Eva Taylor', '543 Redwood Dr', 'eva_taylor', 'secure123'),
        ('Frank Miller', '321 Spruce Rd', 'frank_miller', 'pass987'),
        ('Grace Wilson', '789 Willow Ln', 'grace_wilson', 'password321'),
        ('Henry Turner', '876 Pine Ct', 'henry_turner', 'secure456'),
        ('Ivy Clark', '234 Birch Dr', 'ivy_clark', 'pass321'),
        ('Jack Moore', '567 Cedar Blvd', 'jack_moore', 'password654'),
        ('Kelly Hall', '890 Oak Rd', 'kelly_hall', 'secure654'),
        ('Leo Davis', '123 Elm Blvd', 'leo_davis', 'pass654'),
        ('Mia Martin', '456 Spruce Dr', 'mia_martin', 'password789'),
        ('Nathan Scott', '789 Willow Ct', 'nathan_scott', 'secure987'),
        ('Olivia Adams', '321 Cedar Ave', 'olivia_adams', 'pass234'),
        ('Paula Carter', '654 Birch Rd', 'paula_carter', 'password234'),
        ('Quinn Turner', '987 Maple Blvd', 'quinn_turner', 'secure234');
END;


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'mailing_list')
BEGIN
    CREATE TABLE mailing_list (
        id INT IDENTITY PRIMARY KEY,
        theme VARCHAR(100) NOT NULL,
        mail_descripiton VARCHAR(400) NOT NULL,
        send_date DATE NOT NULL,
        to_subscriber INT NOT NULL,
        FOREIGN KEY (to_subscriber) REFERENCES subscriber(id)
    );

    INSERT INTO mailing_list (theme, mail_descripiton, send_date, to_subscriber) values
        ('Theme important 1', 'Description 1', '2023-11-24', 1),
        ('Theme important 1', 'Description 2', '2023-11-25', 2),
        ('Theme 3', 'Description 3', '2023-11-26', 3),
        ('Theme 4', 'Description 4', '2023-11-27', 4),
        ('Theme important 5', 'Description 5', '2023-11-28', 5),
        ('Theme important 1', 'Description 6', '2023-11-29', 6),
        ('Theme 7', 'Description 7', '2023-11-30', 7),
        ('Theme 10', 'Description 8', '2023-12-01', 8),
        ('Theme important 9', 'Description 9', '2023-12-02', 9),
        ('Theme 10', 'Description 10', '2023-12-03', 10),
        ('Theme 11', 'Description 11', '2023-12-04', 11),
        ('Theme important 1', 'Description 12', '2023-12-05', 12),
        ('Theme 13', 'Description 13', '2023-12-06', 13),
        ('Theme 14', 'Description 14', '2023-12-07', 14),
        ('Theme 15', 'Description 15', '2023-12-08', 15),
        ('Theme 16', 'Description 16', '2023-12-09', 16),
        ('Theme important 17', 'Description 17', '2023-12-10', 17),
        ('Theme important 18', 'Description 18', '2023-12-11', 18),
        ('Theme 19', 'Description 19', '2023-12-12', 19),
        ('Theme 20', 'Description 20', '2023-12-13', 20);
      

END;


select * from subscriber;

-- a) Запит на вибірку

select * from mailing_list;

-- b) запит на вибірку з використанням спеціальних функцій: LIKE, IS NULL, IN, BETWEEN;

SELECT *
FROM mailing_list
WHERE theme LIKE '%important%';

SELECT *
FROM mailing_list
WHERE to_subscriber IS NOT NULL;

SELECT *
FROM mailing_list
WHERE to_subscriber IN (1, 2);

SELECT *
FROM mailing_list
WHERE send_date BETWEEN '2023-01-01' AND '2023-12-6';

-- с) запит зі складним критерієм;

SELECT m.*, s.*
FROM mailing_list m
JOIN subscriber s ON m.to_subscriber = s.id
WHERE m.theme LIKE '%important%'
  AND m.send_date > '2023-01-01'
  AND s.username LIKE 'john%';


-- d) запит з унікальними значеннями;

SELECT * FROM mailing_list WHERE id = 1;

-- e) запит з використанням обчислювального поля;

SELECT * FROM mailing_list WHERE SEND_DATE > '2023-12-01'


-- f) запит з групуванням по заданому полю, використовуючи умову групування;
SELECT 
    m.theme,
    COUNT(*) AS theme_count
FROM mailing_list m
GROUP BY m.theme
HAVING COUNT(*) > 1;

-- g) запит із сортування по заданому полю в порядку зростання та спадання значень;

SELECT *
FROM mailing_list
ORDER BY send_date DESC;


-- h) запит з використанням дій по модифікації записів.
UPDATE mailing_list
SET theme = 'Updated Theme'
WHERE theme LIKE '%important%';

SELECT * 
FROM mailing_list 
WHERE theme LIKE '%Updated%'