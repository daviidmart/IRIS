--truncate table usuarios
--truncate table firewall

INSERT INTO usuarios (usuario, psdw, apik, apis, balance, price, nivel)
VALUES ('davidl','$2a$10$Esev9suggnCmG0QPZ6BAZeS3Vvpzn0NjFxjV3LqycNVU8qi3QvYwG','A3493LAZ','$2a$10$Esev9suggnCmG0QPZ6BAZePjJ69xbQfXgVBcOl3xYlaQnanglcK3y', 10, 0.1, 99);
go
INSERT INTO firewall (usuario, ip)
VALUES ('davidl','::1');


SELECT * FROM usuarios
SELECT * FROM firewall WHERE ip = '::1'