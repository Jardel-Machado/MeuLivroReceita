create schema meulivrodereceitas;

use meulivrodereceitas;

CREATE TABLE `meulivrodereceitas`.`users` (
  `Id` BIGINT NOT NULL AUTO_INCREMENT,
  `CreateOn` DATETIME NOT NULL,
  `Active` TINYINT(1) NOT NULL DEFAULT 1,
  `Name` VARCHAR(100) NOT NULL,
  `Email` VARCHAR(100) NOT NULL,
  `Password` VARCHAR(2000) NOT NULL,
  PRIMARY KEY (`Id`));
  
  