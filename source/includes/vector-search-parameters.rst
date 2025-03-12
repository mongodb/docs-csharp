To create a ``$vectorSearch`` pipeline stage, call the ``VectorSearch()`` method on a
``PipelineStageDefinitionBuilder`` object. The ``VectorSearch()`` method accepts the
following parameters:

.. list-table::
   :header-rows: 1
   :widths: 20 80

   * - Parameter
     - Description

   * - ``field``
     - The field to perform the vector search on.

       **Data type**: ``Expression<Func<TInput, TField>>``

   * - ``queryVector``
     - The encoded vector that will be matched with values from the database.
       Although the data type of this parameter is ``QueryVector``, you can also pass an
       array of floating-point numbers.
       
       **Data type**: `QueryVector <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.QueryVector.html>`__

   * - ``limit``
     - The maximum number of documents to return.
   
       **Data type**: {+int-data-type+}
   
   * - ``options``
     - Configuration options for the vector search operation.
    
       **Data type**: `VectorSearchOptions<TDocument> <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.VectorSearchOptions-1.html>`__

You can use the ``options`` parameter to configure your vector search operation. The
``VectorSearchOptions`` class contains the following properties:

.. list-table::
   :header-rows: 1
   :widths: 20 80

   * - Property
     - Description

   * - ``Exact``
     - Whether the vector search uses the exact nearest neighbor (ENN) algorithm.
       If this property is set to ``false``, the vector search uses the approximate nearest
       neighbor (ANN) algorithm.
   
       **Data type**: {+bool-data-type+}
       **Default**: ``false``

   * - ``Filter``
     - Additional search criteria that the found documents must match.
   
       **Data Type:** `FilterDefinition<TDocument> <{+new-api-root+}/MongoDB.Driver/MongoDB.Driver.FilterDefinition-1.html>`__
       **Default**: ``null``
   
   * - ``IndexName``
     - The index to perform the vector search on.
   
       **Data type**: {+string-data-type+}
       **Default**: ``null``

   * - ``NumberOfCandidates``
     - The number of neighbors to search in the index.
   
       **Data type**: ``int?``
       **Default**: ``null``