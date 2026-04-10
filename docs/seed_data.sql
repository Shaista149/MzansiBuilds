-- ============================================================
-- MzansiBuilds Seed Data Script
-- Run this in SSMS against your MzansiBuilds database
-- after running Update-Database migrations
-- ============================================================
--
-- STEP 0 - DO THIS FIRST BEFORE RUNNING THIS SCRIPT:
-- Register these 5 accounts through the app at /Account/Register
-- Use any password you like (must meet Identity requirements):
--
--   1. dev.thabo@gmail.com
--   2. sara.chen@outlook.com
--   3. marcus.dev@gmail.com
--   4. priya.builds@gmail.com
--   5. luca.codes@outlook.com
--
-- Once all 5 are registered, come back and run this script.
-- ============================================================

-- ============================================================
-- Step 1: Update Developer profiles
-- ============================================================
UPDATE Developers SET
    Username = 'ThaboM',
    Bio = 'Full stack developer from Johannesburg. Building tools that solve real problems for African communities.',
    CreatedAt = '2026-01-10 08:00:00'
WHERE Email = 'dev.thabo@gmail.com';

UPDATE Developers SET
    Username = 'SaraC',
    Bio = 'Frontend developer and UI/UX designer. I love making things look clean and work fast.',
    CreatedAt = '2026-01-15 09:30:00'
WHERE Email = 'sara.chen@outlook.com';

UPDATE Developers SET
    Username = 'MarcusD',
    Bio = 'Backend engineer passionate about APIs, databases and clean architecture.',
    CreatedAt = '2026-01-20 10:00:00'
WHERE Email = 'marcus.dev@gmail.com';

UPDATE Developers SET
    Username = 'PriyaB',
    Bio = 'Mobile developer currently learning Flutter and React Native. Building for accessibility.',
    CreatedAt = '2026-02-01 11:00:00'
WHERE Email = 'priya.builds@gmail.com';

UPDATE Developers SET
    Username = 'LucaV',
    Bio = 'DevOps and cloud engineer. AWS certified. Making deployments boring in the best way.',
    CreatedAt = '2026-02-05 12:00:00'
WHERE Email = 'luca.codes@outlook.com';

-- ============================================================
-- Step 2: Insert Projects
-- ============================================================
DECLARE @thabo INT  = (SELECT DeveloperId FROM Developers WHERE Username = 'ThaboM');
DECLARE @sara INT   = (SELECT DeveloperId FROM Developers WHERE Username = 'SaraC');
DECLARE @marcus INT = (SELECT DeveloperId FROM Developers WHERE Username = 'MarcusD');
DECLARE @priya INT  = (SELECT DeveloperId FROM Developers WHERE Username = 'PriyaB');
DECLARE @luca INT   = (SELECT DeveloperId FROM Developers WHERE Username = 'LucaV');

INSERT INTO Projects (DeveloperId, Title, Description, Stage, SupportNeeded, IsComplete, CreatedAt)
VALUES
(
    @thabo,
    'MzansiBuilds',
    'A platform where developers can build in public, share their progress, log milestones and collaborate with others in the community.',
    'Completed',
    NULL,
    1,
    '2026-01-12 08:00:00'
),
(
    @sara,
    'SkillSwap',
    'A peer-to-peer skill exchange platform for students and graduates. Trade what you know for what you want to learn. No money needed, just time and skills.',
    'In Progress',
    'Looking for a backend developer comfortable with ASP.NET or Node.js to help build the matching algorithm.',
    0,
    '2026-01-18 10:00:00'
),
(
    @marcus,
    'TaxHelper ZA',
    'A simple tax calculator and submission guide for South African freelancers and small businesses. Navigating SARS should not require an accountant.',
    'MVP',
    'Need a frontend developer to improve the UI and a tax professional to verify the calculations.',
    0,
    '2026-01-25 09:00:00'
),
(
    @priya,
    'CampusEats',
    'A food ordering app for university campuses. Students can order from campus vendors, track their order and collect when ready. No more queues.',
    'In Progress',
    'Looking for UI/UX designers and someone with Flutter experience to help build the mobile app.',
    0,
    '2026-02-03 11:00:00'
),
(
    @luca,
    'DeployZA',
    'A beginner-friendly deployment platform for developers. One command to deploy your app with local data residency in South Africa.',
    'Idea',
    'Seeking co-founders and developers interested in cloud infrastructure. Any experience with AWS or Azure welcome.',
    0,
    '2026-02-08 14:00:00'
),
(
    @thabo,
    'CodeMentor SA',
    'Connecting junior developers with senior mentors for free 1-on-1 sessions. Breaking the experience barrier one session at a time.',
    'Completed',
    NULL,
    1,
    '2025-11-05 08:00:00'
),
(
    @sara,
    'AccessibleZA',
    'An accessibility audit tool for South African government and business websites. Many local sites fail basic WCAG standards. This tool makes it easy to identify and fix issues.',
    'Idea',
    'Looking for developers with accessibility expertise and someone who understands WCAG guidelines.',
    0,
    '2026-03-01 10:00:00'
),
(
    @marcus,
    'LoadsheddingAPI',
    'A free and reliable REST API for loadshedding schedules across all South African municipalities. Built because the existing options are unreliable or expensive.',
    'MVP',
    NULL,
    0,
    '2026-02-20 09:00:00'
);

-- ============================================================
-- Step 3: Insert Milestones
-- ============================================================
DECLARE @proj1 INT = (SELECT ProjectId FROM Projects WHERE Title = 'MzansiBuilds');
DECLARE @proj2 INT = (SELECT ProjectId FROM Projects WHERE Title = 'SkillSwap');
DECLARE @proj3 INT = (SELECT ProjectId FROM Projects WHERE Title = 'TaxHelper ZA');
DECLARE @proj4 INT = (SELECT ProjectId FROM Projects WHERE Title = 'CampusEats');
DECLARE @proj6 INT = (SELECT ProjectId FROM Projects WHERE Title = 'CodeMentor SA');
DECLARE @proj8 INT = (SELECT ProjectId FROM Projects WHERE Title = 'LoadsheddingAPI');

INSERT INTO Milestones (ProjectId, Description, AchievedAt)
VALUES
(@proj1, 'Completed database design and EF migrations. All 6 tables created with correct relationships.', '2026-01-15 10:00:00'),
(@proj1, 'Implemented ASP.NET Identity with automatic Developer profile creation on registration.', '2026-01-20 14:00:00'),
(@proj1, 'Built the Live Feed with stage filtering, project cards and collaboration request system.', '2026-02-01 11:00:00'),
(@proj1, 'Completed styling with dark green and black theme. App fully functional.', '2026-02-10 16:00:00'),
(@proj2, 'Finished wireframes and UI design in Figma. Ready to start building.', '2026-01-22 09:00:00'),
(@proj2, 'Built user registration and profile pages. Skills tagging system working.', '2026-02-05 15:00:00'),
(@proj3, 'Core tax calculation logic complete and verified against SARS tables.', '2026-02-01 10:00:00'),
(@proj3, 'Added support for provisional tax and freelancer deductions.', '2026-02-15 13:00:00'),
(@proj4, 'Market research complete. Surveyed 80 students across 3 campuses.', '2026-02-05 09:00:00'),
(@proj4, 'Basic Flutter app structure set up. Login and menu browsing working.', '2026-02-18 14:00:00'),
(@proj6, 'Launched beta with 10 mentor-mentee pairs. Positive early feedback.', '2025-11-20 10:00:00'),
(@proj6, 'Reached 50 completed mentorship sessions. Platform marked complete.', '2025-12-15 16:00:00'),
(@proj8, 'Scraped and normalised loadshedding data for all 257 municipalities.', '2026-02-22 11:00:00'),
(@proj8, 'API live with rate limiting and caching. Handling 500 requests per day.', '2026-03-01 15:00:00');

-- ============================================================
-- Step 4: Insert Comments
-- ============================================================
DECLARE @proj5 INT = (SELECT ProjectId FROM Projects WHERE Title = 'DeployZA');
DECLARE @proj7 INT = (SELECT ProjectId FROM Projects WHERE Title = 'AccessibleZA');

INSERT INTO Comments (ProjectId, DeveloperId, Content, CreatedAt)
VALUES
(@proj1, @sara,   'This is exactly what the dev community needs. Love the build in public concept!', '2026-02-12 09:00:00'),
(@proj1, @marcus, 'The collaboration request feature is a great touch. Looking forward to seeing this grow.', '2026-02-13 11:00:00'),
(@proj2, @thabo,  'Great idea Sara! Skill exchange is really needed for students who cannot afford bootcamps.', '2026-01-20 10:00:00'),
(@proj2, @priya,  'Would love to swap Flutter skills for some backend knowledge. This is exactly what I need!', '2026-02-06 14:00:00'),
(@proj3, @luca,   'SARS documentation is a nightmare. A tool like this would have saved me hours last tax season.', '2026-01-28 09:00:00'),
(@proj3, @sara,   'Really clean idea Marcus. Would be great to add an e-filing integration eventually.', '2026-02-02 13:00:00'),
(@proj4, @thabo,  'CampusEats is solving a real problem. The queues at campus cafeterias are brutal!', '2026-02-10 10:00:00'),
(@proj4, @marcus, 'Have you looked at integrating with SnapScan or Zapper for payments? Perfect for campus use.', '2026-02-19 15:00:00'),
(@proj5, @thabo,  'This would be a game changer for developers who want local data residency without the complexity.', '2026-02-10 09:00:00'),
(@proj5, @marcus, 'Have you considered using the Cape Town AWS region as the backend? Infrastructure already exists.', '2026-02-11 14:00:00'),
(@proj8, @priya,  'Finally a reliable loadshedding API! The existing ones go down right when you need them most.', '2026-02-25 10:00:00'),
(@proj8, @thabo,  'This is genuinely useful. Would you consider adding push notifications for schedule changes?', '2026-03-02 09:00:00'),
(@proj7, @luca,   'So important. I audited a government portal recently and it failed almost every WCAG checkpoint.', '2026-03-03 11:00:00');

-- ============================================================
-- Step 5: Insert Collaboration Requests
-- ============================================================
INSERT INTO CollaborationRequests (ProjectId, RequesterId, Message, Status, CreatedAt)
VALUES
(
    @proj2, @marcus,
    'I can help with the backend matching algorithm. I have experience building recommendation systems in C#.',
    'Accepted',
    '2026-01-25 10:00:00'
),
(
    @proj2, @luca,
    'I can set up the cloud infrastructure and CI/CD pipeline for SkillSwap. Happy to contribute.',
    'Pending',
    '2026-02-08 14:00:00'
),
(
    @proj3, @sara,
    'I can help redesign the frontend. The tax calculator needs a cleaner UI to feel trustworthy.',
    'Accepted',
    '2026-02-03 11:00:00'
),
(
    @proj4, @marcus,
    'I can build out the backend API for order management and vendor dashboards.',
    'Pending',
    '2026-02-20 09:00:00'
),
(
    @proj5, @thabo,
    'Very interested in this. I can help architect the platform and build the CLI tool.',
    'Pending',
    '2026-02-12 10:00:00'
),
(
    @proj8, @priya,
    'Could I build a Flutter package that wraps this API? Would make it easy for mobile devs to use.',
    'Accepted',
    '2026-02-26 13:00:00'
);

-- ============================================================
-- Step 6: Insert Celebrations for completed projects
-- ============================================================
INSERT INTO Celebrations (ProjectId, DeveloperId, CelebratedAt)
VALUES
(@proj1, @thabo, '2026-02-10 16:00:00'),
(@proj6, @thabo, '2025-12-15 16:00:00');

-- ============================================================
-- Verification queries (uncomment to run)
-- ============================================================
-- SELECT * FROM Developers;
-- SELECT * FROM Projects ORDER BY CreatedAt;
-- SELECT COUNT(*) AS MilestoneCount FROM Milestones;
-- SELECT COUNT(*) AS CommentCount FROM Comments;
-- SELECT COUNT(*) AS CollabCount FROM CollaborationRequests;
-- SELECT * FROM Celebrations;
-- ============================================================