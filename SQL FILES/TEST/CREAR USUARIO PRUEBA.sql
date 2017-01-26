--truncate table usuarios
--truncate table firewall

INSERT INTO usuarios (usuario, psdw, apik, apis, balance, price, nivel)
VALUES ('davidl','$2a$10$Esev9suggnCmG0QPZ6BAZeR6JHZ4GG74oFJVq9kXNR2HiBMCdhhyK','A3493LAZ','CZYC5QXAXVDFVZ', 10, 0.1, 99);
go
INSERT INTO firewall (usuario, ip)
VALUES ('davidl','::1');


SELECT * FROM usuarios
SELECT * FROM firewall WHERE ip = '::1'
SELECT * FROM servidores