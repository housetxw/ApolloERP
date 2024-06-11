set global log_bin_trust_function_creators = 1;

use product;

DELIMITER ;;
CREATE DEFINER=`ApolloErp`@`%` FUNCTION `next_val`(v_seq_name VARCHAR(50)) RETURNS int
begin  

  declare cvalue integer;

    set cvalue = 0;         

    select current_val + increment_val into cvalue  from dim_sequence where seq_name = v_seq_name for update;   

    update dim_sequence set current_val = cvalue  where seq_name = v_seq_name;  

    return cvalue;  

END ;;
DELIMITER ;

use coupon;

DELIMITER ;;
CREATE DEFINER=`ApolloErp`@`%` FUNCTION `rand_num`(

`start_num` int(11) ,`end_num` int(11) ) RETURNS int
    SQL SECURITY INVOKER
BEGIN

    RETURN FLOOR(start_num + RAND() * (end_num - start_num + 1));

END ;;
DELIMITER ;

