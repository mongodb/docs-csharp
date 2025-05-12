The client's default read-preference settings. ``MaxStaleness`` represents the
longest replication lag, in wall-clock time, that a secondary can experience and
still be eligible for server selection. The default value is ``ReadPreference.Primary``.
Specifying ``-1`` means no maximum.
See :ref:`read preference <read-preference>` for more information.