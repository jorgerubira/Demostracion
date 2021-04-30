UPDATE cov_h_camas SET id_hospital = (SELECT id FROM cov_m_hospitales h WHERE h.hospital = cov_h_camas.hospital)

ALTER TABLE cov_h_camas DROP COLUMN hospital;
