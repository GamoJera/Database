-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 18, 2023 at 04:26 PM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.0.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `user`
--

-- --------------------------------------------------------

--
-- Table structure for table `attendancerec`
--

CREATE TABLE `attendancerec` (
  `firstname` varchar(20) NOT NULL,
  `lastname` varchar(20) NOT NULL,
  `gradelevel` varchar(20) NOT NULL,
  `section` varchar(20) NOT NULL,
  `attendance` varchar(20) NOT NULL,
  `fines` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `attendancerec`
--

INSERT INTO `attendancerec` (`firstname`, `lastname`, `gradelevel`, `section`, `attendance`, `fines`) VALUES
('', '', '', '', '', ''),
('', '', '', '', '', ''),
('', '', '', '', '', '');

-- --------------------------------------------------------

--
-- Table structure for table `attendance_rec`
--

CREATE TABLE `attendance_rec` (
  `firstname` varchar(20) NOT NULL,
  `lastname` varchar(20) NOT NULL,
  `gradelevel` varchar(20) NOT NULL,
  `section` varchar(20) NOT NULL,
  `date` date DEFAULT NULL,
  `eventname` varchar(50) NOT NULL,
  `attendance` varchar(20) NOT NULL,
  `fines` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `attendance_rec`
--

INSERT INTO `attendance_rec` (`firstname`, `lastname`, `gradelevel`, `section`, `date`, `eventname`, `attendance`, `fines`) VALUES
('Jerameel', 'Gamo', 'Grade 12', 'Mabini', '2023-12-18', 'xmas Party', 'Present', '30');

-- --------------------------------------------------------

--
-- Table structure for table `event`
--

CREATE TABLE `event` (
  `eventname` varchar(50) NOT NULL,
  `location` varchar(50) NOT NULL,
  `date` date NOT NULL,
  `gradelevel` varchar(20) NOT NULL,
  `fines` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `event`
--

INSERT INTO `event` (`eventname`, `location`, `date`, `gradelevel`, `fines`) VALUES
('X mas party', 'SLSU', '2023-12-18', 'Grade 12', '30');

-- --------------------------------------------------------

--
-- Table structure for table `login`
--

CREATE TABLE `login` (
  `Username` varchar(20) NOT NULL,
  `Password` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `login`
--

INSERT INTO `login` (`Username`, `Password`) VALUES
('admin', 'password'),
('admin1', 'admin'),
('jerameel', '12345678'),
('jerameel12', 'jerameel123456'),
('qq', 'qq'),
('juliet', 'lincuna'),
('jaja', 'jaja'),
('haha', 'haha'),
('rr', 'rr');

-- --------------------------------------------------------

--
-- Table structure for table `studentinfo`
--
-- Error reading structure for table user.studentinfo: #1932 - Table 'user.studentinfo' doesn't exist in engine
-- Error reading data for table user.studentinfo: #1064 - You have an error in your SQL syntax; check the manual that corresponds to your MariaDB server version for the right syntax to use near 'FROM `user`.`studentinfo`' at line 1

-- --------------------------------------------------------

--
-- Table structure for table `student_info`
--

CREATE TABLE `student_info` (
  `firstname` varchar(20) NOT NULL,
  `lastname` varchar(20) NOT NULL,
  `contactNo` int(20) NOT NULL,
  `address` varchar(20) NOT NULL,
  `gradelevel` varchar(20) NOT NULL,
  `section` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `student_info`
--

INSERT INTO `student_info` (`firstname`, `lastname`, `contactNo`, `address`, `gradelevel`, `section`) VALUES
('Jerameel ', 'Gamo', 2147483647, 'Nava Hinunangan', 'Grade 12', 'mabini');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
