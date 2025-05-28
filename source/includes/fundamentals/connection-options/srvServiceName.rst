The SRV service name. The driver uses the service name to create the SRV URI, which mathces
the following format:

.. code-block::
    
    _{srvServiceName}._tcp.{hostname}.{domainname}

The default value is ``"mongodb"``.