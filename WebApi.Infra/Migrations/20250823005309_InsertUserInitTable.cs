using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace WebApi.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InsertUserInitTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = new StringBuilder();
            sql.Append(@"
            -- ===========================================
            -- Seed initial data (MySQL) - Authentication, User, Curiosity
            -- Idempotent inserts with FK associations
            -- ===========================================

            -- Ensure session vars
            SET @auth_admin_id := NULL;
            SET @auth_diego_id := NULL;
            SET @auth_stephanie_id := NULL;
            SET @auth_jeffrey_id := NULL;
            SET @auth_darin_id := NULL;
            SET @auth_andrew_id := NULL;
            SET @auth_valerie_id := NULL;

            -- ========== AUTHENTICATION ==========

            -- ADMIN
            SELECT Id INTO @auth_admin_id FROM `Authentication` WHERE `Login`='admin@curiosity.com';
            SET @auth_admin_id := COALESCE(@auth_admin_id, 0);
            INSERT INTO `Authentication` (`Login`,`Password`,`Profile`)
            SELECT 'admin@curiosity.com','240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9','Admin'
            WHERE @auth_admin_id = 0;
            SELECT Id INTO @auth_admin_id FROM `Authentication` WHERE `Login`='admin@curiosity.com';

            -- DIEGO
            SELECT Id INTO @auth_diego_id FROM `Authentication` WHERE `Login`='dalves@curiosity.com';
            SET @auth_diego_id := COALESCE(@auth_diego_id, 0);
            INSERT INTO `Authentication` (`Login`,`Password`,`Profile`)
            SELECT 'dalves@curiosity.com','d68089c77e65ee87eb9504f68212289f6dcb758fae1955a824018a67c5cb2e2f','User'
            WHERE @auth_diego_id = 0;
            SELECT Id INTO @auth_diego_id FROM `Authentication` WHERE `Login`='dalves@curiosity.com';

            -- MOCK USERS (password: 123456)
            SELECT Id INTO @auth_stephanie_id FROM `Authentication` WHERE `Login`='stephanienichols@gmail.com';
            SET @auth_stephanie_id := COALESCE(@auth_stephanie_id, 0);
            INSERT INTO `Authentication` (`Login`,`Password`,`Profile`)
            SELECT 'stephanienichols@gmail.com','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92','User'
            WHERE @auth_stephanie_id = 0;
            SELECT Id INTO @auth_stephanie_id FROM `Authentication` WHERE `Login`='stephanienichols@gmail.com';

            SELECT Id INTO @auth_jeffrey_id FROM `Authentication` WHERE `Login`='jeffrey_kane@yahoo.com';
            SET @auth_jeffrey_id := COALESCE(@auth_jeffrey_id, 0);
            INSERT INTO `Authentication` (`Login`,`Password`,`Profile`)
            SELECT 'jeffrey_kane@yahoo.com','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92','User'
            WHERE @auth_jeffrey_id = 0;
            SELECT Id INTO @auth_jeffrey_id FROM `Authentication` WHERE `Login`='jeffrey_kane@yahoo.com';

            SELECT Id INTO @auth_darin_id FROM `Authentication` WHERE `Login`='darinmiller01@gmail.com';
            SET @auth_darin_id := COALESCE(@auth_darin_id, 0);
            INSERT INTO `Authentication` (`Login`,`Password`,`Profile`)
            SELECT 'darinmiller01@gmail.com','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92','User'
            WHERE @auth_darin_id = 0;
            SELECT Id INTO @auth_darin_id FROM `Authentication` WHERE `Login`='darinmiller01@gmail.com';

            SELECT Id INTO @auth_andrew_id FROM `Authentication` WHERE `Login`='andrewstuart@outlook.com';
            SET @auth_andrew_id := COALESCE(@auth_andrew_id, 0);
            INSERT INTO `Authentication` (`Login`,`Password`,`Profile`)
            SELECT 'andrewstuart@outlook.com','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92','User'
            WHERE @auth_andrew_id = 0;
            SELECT Id INTO @auth_andrew_id FROM `Authentication` WHERE `Login`='andrewstuart@outlook.com';

            SELECT Id INTO @auth_valerie_id FROM `Authentication` WHERE `Login`='valerie_aguilar@gmail.com';
            SET @auth_valerie_id := COALESCE(@auth_valerie_id, 0);
            INSERT INTO `Authentication` (`Login`,`Password`,`Profile`)
            SELECT 'valerie_aguilar@gmail.com','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92','User'
            WHERE @auth_valerie_id = 0;
            SELECT Id INTO @auth_valerie_id FROM `Authentication` WHERE `Login`='valerie_aguilar@gmail.com';

            -- ========== USERS ==========

            -- ADMIN user
            INSERT INTO `User` (`Name`,`Age`,`Email`,`Address`,`IsActive`,`CreatedAt`,`IdAuthentication`)
            SELECT 'System Administrator',30,'admin@curiosity.com','Headquarters',1,UTC_TIMESTAMP(),@auth_admin_id
            WHERE NOT EXISTS (SELECT 1 FROM `User` WHERE `Email`='admin@curiosity.com');

            -- DIEGO
            INSERT INTO `User` (`Name`,`Age`,`Email`,`Address`,`IsActive`,`CreatedAt`,`IdAuthentication`)
            SELECT 'Diego Alves Bassualdo',37,'dalves@curiosity.com','Campinas, SP',1,UTC_TIMESTAMP(),@auth_diego_id
            WHERE NOT EXISTS (SELECT 1 FROM `User` WHERE `Email`='dalves@curiosity.com');

            -- MOCK USERS (statuses conforme mock: últimos 4 ativos, Valerie inativa)
            INSERT INTO `User` (`Name`,`Age`,`Email`,`Address`,`IsActive`,`CreatedAt`,`IdAuthentication`)
            SELECT 'Stephanie Nichols',NULL,'stephanienichols@gmail.com',NULL,1,UTC_TIMESTAMP(),@auth_stephanie_id
            WHERE NOT EXISTS (SELECT 1 FROM `User` WHERE `Email`='stephanienichols@gmail.com');

            INSERT INTO `User` (`Name`,`Age`,`Email`,`Address`,`IsActive`,`CreatedAt`,`IdAuthentication`)
            SELECT 'Jeffrey Kane',NULL,'jeffrey_kane@yahoo.com',NULL,1,UTC_TIMESTAMP(),@auth_jeffrey_id
            WHERE NOT EXISTS (SELECT 1 FROM `User` WHERE `Email`='jeffrey_kane@yahoo.com');

            INSERT INTO `User` (`Name`,`Age`,`Email`,`Address`,`IsActive`,`CreatedAt`,`IdAuthentication`)
            SELECT 'Darin Miller',NULL,'darinmiller01@gmail.com',NULL,1,UTC_TIMESTAMP(),@auth_darin_id
            WHERE NOT EXISTS (SELECT 1 FROM `User` WHERE `Email`='darinmiller01@gmail.com');

            INSERT INTO `User` (`Name`,`Age`,`Email`,`Address`,`IsActive`,`CreatedAt`,`IdAuthentication`)
            SELECT 'Andrew Stuart',NULL,'andrewstuart@outlook.com',NULL,1,UTC_TIMESTAMP(),@auth_andrew_id
            WHERE NOT EXISTS (SELECT 1 FROM `User` WHERE `Email`='andrewstuart@outlook.com');

            INSERT INTO `User` (`Name`,`Age`,`Email`,`Address`,`IsActive`,`CreatedAt`,`IdAuthentication`)
            SELECT 'Valerie Aguilar',NULL,'valerie_aguilar@gmail.com',NULL,0,UTC_TIMESTAMP(),@auth_valerie_id
            WHERE NOT EXISTS (SELECT 1 FROM `User` WHERE `Email`='valerie_aguilar@gmail.com');

            -- ========== CURIOSITY ==========

            -- Helper: insert curiosity if missing for a given email
            -- (MySQL: insert-select pattern with NOT EXISTS)
            INSERT INTO `Curiosity` (`UserId`,`OtherInfo`,`Interests`,`Feelings`,`CreatedAt`)
            SELECT u.Id,
                   'Initial seeded data.',
                   'Technology; Reading; Music.',
                   'Positive and curious.',
                   UTC_TIMESTAMP()
            FROM `User` u
            WHERE u.Email='admin@curiosity.com'
              AND NOT EXISTS (SELECT 1 FROM `Curiosity` c WHERE c.UserId=u.Id);

            INSERT INTO `Curiosity` (`UserId`,`OtherInfo`,`Interests`,`Feelings`,`CreatedAt`)
            SELECT u.Id,
                   'Owner of the repository and primary operator.',
                   'Software Engineering; AWS; .NET; Angular.',
                   'Motivated to deliver the assessment.',
                   UTC_TIMESTAMP()
            FROM `User` u
            WHERE u.Email='dalves@curiosity.com'
              AND NOT EXISTS (SELECT 1 FROM `Curiosity` c WHERE c.UserId=u.Id);

            INSERT INTO `Curiosity` (`UserId`,`OtherInfo`,`Interests`,`Feelings`,`CreatedAt`)
            SELECT u.Id,'Sample record from mock.','Art; Travel.','Optimistic.',UTC_TIMESTAMP()
            FROM `User` u
            WHERE u.Email='stephanienichols@gmail.com'
              AND NOT EXISTS (SELECT 1 FROM `Curiosity` c WHERE c.UserId=u.Id);

            INSERT INTO `Curiosity` (`UserId`,`OtherInfo`,`Interests`,`Feelings`,`CreatedAt`)
            SELECT u.Id,'Sample record from mock.','Outdoors; Sports.','Curious.',UTC_TIMESTAMP()
            FROM `User` u
            WHERE u.Email='jeffrey_kane@yahoo.com'
              AND NOT EXISTS (SELECT 1 FROM `Curiosity` c WHERE c.UserId=u.Id);

            INSERT INTO `Curiosity` (`UserId`,`OtherInfo`,`Interests`,`Feelings`,`CreatedAt`)
            SELECT u.Id,'Sample record from mock.','Cooking; Photography.','Calm.',UTC_TIMESTAMP()
            FROM `User` u
            WHERE u.Email='darinmiller01@gmail.com'
              AND NOT EXISTS (SELECT 1 FROM `Curiosity` c WHERE c.UserId=u.Id);

            INSERT INTO `Curiosity` (`UserId`,`OtherInfo`,`Interests`,`Feelings`,`CreatedAt`)
            SELECT u.Id,'Sample record from mock.','Finance; Reading.','Focused.',UTC_TIMESTAMP()
            FROM `User` u
            WHERE u.Email='andrewstuart@outlook.com'
              AND NOT EXISTS (SELECT 1 FROM `Curiosity` c WHERE c.UserId=u.Id);

            INSERT INTO `Curiosity` (`UserId`,`OtherInfo`,`Interests`,`Feelings`,`CreatedAt`)
            SELECT u.Id,'Sample record from mock.','Travel; Languages.','Reflective.',UTC_TIMESTAMP()
            FROM `User` u
            WHERE u.Email='valerie_aguilar@gmail.com'
              AND NOT EXISTS (SELECT 1 FROM `Curiosity` c WHERE c.UserId=u.Id);

            ");

            migrationBuilder.Sql(sql.ToString());
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
 
        }
    }
}
